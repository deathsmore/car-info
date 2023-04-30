using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Contracts.ApplicationServices.Interfaces;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Specifications;
using DVG.AP.Cms.CarInfo.Application.Features.NewCarArticle.Factories;
using DVG.AP.Cms.CarInfo.Application.Features.Specifications.CarImage;
using DVG.AP.Cms.CarInfo.Domain.Entities.CommonEntities;
using DVG.AP.Cms.CarInfo.Domain.Enums;
using DVG.AutoPortal.Core.Exceptions;
using DVG.AutoPortal.Core.Extensions;
using DVG.AutoPortal.Core.GuardClauses;
using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.CarInfo.Commands.CarInfoUpdate
{
    public class CarInfoUpdateCommandHandler : IRequestHandler<CarInfoUpdateCommand>
    {
        private readonly ICarInfoRepository _carInfoRepository;
        private readonly IRepository<Domain.Entities.CarImage> _carImageRepository;
        private readonly IMapper _mapper;
        private readonly INewCarArticleRepository _newCarArticleRepository;
        private readonly NewCarArticleFactory _newCarArticleFactory;
        private readonly IUrlRepository _urlRepository;
        private readonly INewCarModelRepository _newCarModelRepository;
        private readonly INewCarVariantRepository _newCarVariantRepository;
        private readonly IModelPropertySummariesService _modelPropertySummariesService;

        public CarInfoUpdateCommandHandler(
            ICarInfoRepository carInfoRepository,
            IRepository<Domain.Entities.CarImage> carImageRepository,
            IMapper mapper,
            INewCarArticleRepository newCarArticleRepository,
            NewCarArticleFactory newCarArticleFactory,
            IUrlRepository urlRepository,
            INewCarModelRepository newCarModelRepository,
            INewCarVariantRepository newCarVariantRepository,
            IModelPropertySummariesService modelPropertySummariesService)
        {
            _carInfoRepository = carInfoRepository;
            _carImageRepository = carImageRepository;
            _mapper = mapper;
            _newCarArticleRepository = newCarArticleRepository;
            _newCarArticleFactory = newCarArticleFactory;
            _urlRepository = urlRepository;
            _newCarModelRepository = newCarModelRepository;
            _newCarVariantRepository = newCarVariantRepository;
            _modelPropertySummariesService = modelPropertySummariesService;
        }

        public async Task<Unit> Handle(CarInfoUpdateCommand request, CancellationToken cancellationToken)
        {
            await Guard.Against.Validate(request.CarInfoUpdate, new CarInfoUpdateCommandValidation());

            var checkExistedSpec = new GetCarInfosSpec();
            checkExistedSpec.GetByVariant(request.CarInfoUpdate.VariantId);
            checkExistedSpec.GetByYear(request.CarInfoUpdate.Year);
            var carInfoCheckExisted = await _carInfoRepository.ListAsync(checkExistedSpec);

            if (carInfoCheckExisted.Any(x=> x.Id != request.CarInfoUpdate.Id))
            {
                throw new ConflictException(nameof(Domain.Entities.CarInfo), new { request.CarInfoUpdate.VariantId, request.CarInfoUpdate.Year });
            }

            var carInfo = await _carInfoRepository.GetBySpecAsync(new CarInfoDetailSpecForUpdate(request.CarInfoUpdate.Id), cancellationToken);
            NotFoundException.NotFound(carInfo, name: nameof(Domain.Entities.CarInfo),
                key: request.CarInfoUpdate.Id);
            int oldModelId = carInfo!.Variant.ModelId;

            _mapper.Map(request.CarInfoUpdate, carInfo, typeof(CarInfoUpdate),
                typeof(Domain.Entities.CarInfo));

            var latestCarInfo = await _carInfoRepository.GetLatest(request.CarInfoUpdate.VariantId);
            if (latestCarInfo == null)
            {
                carInfo.IsLatest = true;
            }
            else if (latestCarInfo.Year < carInfo.Year && latestCarInfo.Id != carInfo.Id)
            {
                carInfo.IsLatest = true;
                latestCarInfo.IsLatest = false;
                await _carInfoRepository.UpdateAsync(latestCarInfo);
            }
            await _carInfoRepository.UpdateAsync(carInfo, cancellationToken);

            #region Update lại price range cho các bài newCar liên quan
            //var newCarBrandEntity = await _newCarArticleRepository.GetByObjectIdAndType(request.CarInfoUpdate.BrandId, NewCarArticleType.Brand);
            //if (newCarBrandEntity != null)
            //{
            //    NewCarArticleAbstract newCarBrand = _newCarArticleFactory.CreateNewCarArticle(NewCarArticleType.Brand);
            //    newCarBrand.SetNewCarArticle(newCarBrandEntity);
            //    await newCarBrand.CalcPriceRange();
            //    await _newCarArticleRepository.UpdateAsync(newCarBrandEntity, cancellationToken);
            //}

            var newCarModelEntity = await _newCarModelRepository.GetByModelId(request.CarInfoUpdate.ModelId);
            if (newCarModelEntity != null)
            {
                NewCarArticleAbstract newCarModel = _newCarArticleFactory.CreateNewCarArticle(NewCarArticleType.Model);
                newCarModel.SetNewCarEntity(newCarModelEntity);
                await newCarModel.CalcPriceRange();
                await _newCarModelRepository.UpdateAsync(newCarModelEntity);
            }

            var newCarVariantEntity = await _newCarVariantRepository.GetByVariantId(request.CarInfoUpdate.VariantId);
            if (newCarVariantEntity != null)
            {
                NewCarArticleAbstract newCarVariant = _newCarArticleFactory.CreateNewCarArticle(NewCarArticleType.Variant);
                newCarVariant.SetNewCarEntity(newCarVariantEntity);
                await newCarVariant.CalcPriceRange();
                await _newCarVariantRepository.UpdateAsync(newCarVariantEntity);
            }
            #endregion

            #region Xử lý url db common
            var url = await _urlRepository.GetByObject(request.CarInfoUpdate.Id, ObjectType.CarInfo);

            if (url == null)
            {
                var slugExisted = await _urlRepository.GetBySlug(request.CarInfoUpdate.Url);
                await _urlRepository.AddAsync(new Url
                {
                    ObjectId = request.CarInfoUpdate.Id,
                    ObjectType = ObjectType.CarInfo,
                    Slug = slugExisted == null ? request.CarInfoUpdate.Url : $"{request.CarInfoUpdate.Url}-{request.CarInfoUpdate.Id}-{ObjectType.CarInfo.GetHashCode()}",
                    CreatedDate = DateTime.Now
                }, cancellationToken);
            }
            #endregion

            #region Sync du lieu bang ModelPropertySummaries
            //var modelPropertySummariesTask = _modelPropertySummariesService.SyncSummarySpec(request.CarInfoUpdate.ModelId);
            //Task oldModelPropertySummariesTask = oldModelId == request.CarInfoUpdate.ModelId ? Task.CompletedTask : _modelPropertySummariesService.SyncSummarySpec(oldModelId);//Check TH update carInfo co thay doi model

            //await Task.WhenAll(new Task[] { modelPropertySummariesTask, oldModelPropertySummariesTask });
            await _modelPropertySummariesService.SyncSummarySpec(request.CarInfoUpdate.ModelId);
            if (oldModelId != request.CarInfoUpdate.ModelId)
            {
                await _modelPropertySummariesService.SyncSummarySpec(oldModelId);
            }
            #endregion
            return Unit.Value;
        }
    }
}
