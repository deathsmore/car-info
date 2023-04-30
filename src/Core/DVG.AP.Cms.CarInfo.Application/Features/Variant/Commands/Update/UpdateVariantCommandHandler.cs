using AutoMapper;
using DVG.AP.Cms.CarInfo.Application.Contracts.Persistence;
using DVG.AutoPortal.Core.Exceptions;
using DVG.AutoPortal.Core.Extensions;
using DVG.AutoPortal.Core.GuardClauses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Features.Variant.Commands.Update
{
    public class UpdateVariantCommandHandler : IRequestHandler<UpdateVariantCommand, int>
    {
        private readonly IVariantRepository _variantRepository;
        private readonly IMapper _mapper;
        public UpdateVariantCommandHandler(IVariantRepository variantRepository, IMapper mapper)
        {
            _variantRepository = variantRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(UpdateVariantCommand request, CancellationToken cancellationToken)
        {
            await Guard.Against.Validate(request.Variant, new UpdateVariantCommandValidation());

            var variant = await _variantRepository.GetByIdAsync(request.Variant.Id, cancellationToken);
            NotFoundException.NotFound(variant, name: nameof(Domain.Entities.Variant), key: request.Variant.Id);

            _mapper.Map(request.Variant, variant, typeof(VariantForUpdate), typeof(Domain.Entities.Variant));

            await _variantRepository.UpdateAsync(variant, cancellationToken);
            await _variantRepository.SaveChangesAsync(cancellationToken);
            return variant.Id;
        }
    }
}
