using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AutoPortal.Core.Extensions;
using DVG.AutoPortal.Core.GuardClauses;
using MediatR;

namespace DVG.AP.Cms.CarInfo.Application.Features.Variant.Commands.Create
{
    public class CreateVariantCommandHandler : IRequestHandler<CreateVariantCommand, int>
    {
        private readonly IRepository<Domain.Entities.Variant> _variantRepository;
        private readonly IMapper _mapper;
        public CreateVariantCommandHandler(
            IRepository<Domain.Entities.Variant> variantRepository,
            IMapper mapper
        )
        {
            _variantRepository = variantRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateVariantCommand request, CancellationToken cancellationToken)
        {
            await Guard.Against.Validate(request.Variant, new CreateVariantCommandValidation());

            var variant = _mapper.Map<Domain.Entities.Variant>(request.Variant);
            variant = await _variantRepository.AddAsync(variant, cancellationToken);
            await _variantRepository.SaveChangesAsync();
            return variant.Id;
        }
    }
}
