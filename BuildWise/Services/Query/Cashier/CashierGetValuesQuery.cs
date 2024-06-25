using AutoMapper;
using BuildWise.DTO.Address;
using BuildWise.DTO.Cashier;
using BuildWise.DTO.Construction;
using BuildWise.DTO.Person;
using BuildWise.Entities;
using BuildWise.Interfaces.Repository;
using BuildWise.Payload.Cashier;
using BuildWise.Payload.Construction;
using BuildWise.Services.Query.Construction;
using FluentValidation;
using MediatR;
using System;
using static BuildWise.Enum.SaleEnum;

namespace BuildWise.Services.Query.Cashier
{
    public class CashierGetValuesQuery : IRequest<CashierGetValuesDTO>
    {
        public CashierGetValuesQuery(int id)
        {
            Payload = new CashierGetValuesPayload { Id = id };
        }
        public CashierGetValuesPayload Payload { get; set;  }
    }
    internal class CashierGetValuesQueryHandler : IRequestHandler<CashierGetValuesQuery, CashierGetValuesDTO>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        //private readonly IValidator<CashierGetValuesPayload> _validator;
        public CashierGetValuesQueryHandler(
            IUnitOfWork uow,
            IMapper mapper
            //,
            //IValidator<ConstructionGetByIdPayload> validator
            )
        {
            _uow = uow;
            //_validator = validator;
            _mapper = mapper;
        }
        public async Task<CashierGetValuesDTO> Handle(CashierGetValuesQuery request, CancellationToken cancellationToken)
        {
            //await _validator.ValidateAndThrowAsync(request.Payload);
            Entities.Cashier cashier = await _uow.Cashier.GetByIdAsync(request.Payload.Id);
            DateTime endDate = DateTime.UtcNow;

            CashierGetValuesDTO dto = new CashierGetValuesDTO();
            dto.Values.Pix = await _uow.Cashier.GetAmountPaidByReceivementMethod(
                cashier.CreatedAt,
                endDate,
                ESaleReceivementMethod.Pix);
            dto.Values.DebitCard = await _uow.Cashier.GetAmountPaidByReceivementMethod(
                cashier.CreatedAt,
                endDate,
                ESaleReceivementMethod.DebitCard);
            dto.Values.CreditCard = await _uow.Cashier.GetAmountPaidByReceivementMethod(
                cashier.CreatedAt,
                endDate,
                ESaleReceivementMethod.CreditCard);
            dto.Values.Money = await _uow.Cashier.GetAmountPaidByReceivementMethod(
                cashier.CreatedAt,
                endDate,
                ESaleReceivementMethod.Money);
            
            dto.OpeningDate = FromUTCToTimezone(cashier); 

            return dto;
        }

        private static DateTime FromUTCToTimezone(Entities.Cashier cashier)
        {
            TimeZoneInfo timezone = TimeZoneInfo.Local;
            DateTime timezoneDate = TimeZoneInfo.ConvertTimeFromUtc(cashier.CreatedAt, timezone);
            return timezoneDate;
        }
    }

}
