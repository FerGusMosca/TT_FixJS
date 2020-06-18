using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fwk.Main.BusinessEntities.Positions;
using Fwk.Main.Common.Enums;
using Fwk.Main.DataAccess;

namespace Fwk.Main.DataAccessLayer.Profiles
{
    //public class PositionProfile: Profile
    //{
    //    public override string ProfileName
    //    {
    //        get { return "PositionProfile"; }
    //    }

    //    protected override void Configure()
    //    {
    //        CreateMap<positions, Position>()
    //            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
    //            .ForMember(dest => dest.PosId, opt => opt.MapFrom(src => src.pos_id))
    //            .ForMember(dest => dest.Symbol, opt => opt.MapFrom(src => src.symbol))
    //            .ForMember(dest => dest.Side, opt => opt.MapFrom(src => (Side) Convert.ToChar(src.side)))
    //            .ForMember(dest => dest.PosStatus, opt => opt.MapFrom(src => (PositionStatus)Convert.ToChar(src.pos_status)))
    //            .ForMember(dest => dest.Exchange, opt => opt.MapFrom(src => src.exchange))
    //            .ForMember(dest => dest.QuantityType, opt => opt.MapFrom(src => (QuantityType) src.quantity_type))
    //            .ForMember(dest => dest.PriceType, opt => opt.MapFrom(src => (PriceType) src.price_type))
    //            .ForMember(dest => dest.Qty, opt => opt.MapFrom(src => src.qty))
    //            .ForMember(dest => dest.CashQty, opt => opt.MapFrom(src => src.cash_qty))
    //            .ForMember(dest => dest.Percent, opt => opt.MapFrom(src => src.percent))
    //            .ForMember(dest => dest.CumQty, opt => opt.MapFrom(src => src.cum_qty))
    //            .ForMember(dest => dest.LeavesQty, opt => opt.MapFrom(src => src.leaves_qty))
    //            .ForMember(dest => dest.AvgPx, opt => opt.MapFrom(src => src.avg_px))
    //            .ForMember(dest => dest.LastQty, opt => opt.MapFrom(src => src.last_qty))
    //            .ForMember(dest => dest.LastMkt, opt => opt.MapFrom(src => src.last_mkt))
    //            .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.orders))
    //            .ForMember(dest => dest.ExecutionReports, opt => opt.MapFrom(src => src.execution_reports))
    //            ;

    //        CreateMap<Position, positions>()
    //            .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id))
    //            .ForMember(dest => dest.pos_id, opt => opt.MapFrom(src => src.PosId))
    //            .ForMember(dest => dest.symbol, opt => opt.MapFrom(src => src.Symbol))
    //            .ForMember(dest => dest.side, opt => opt.MapFrom(src => (char)src.Side))
    //            .ForMember(dest => dest.pos_status, opt => opt.MapFrom(src => (char)src.PosStatus))
    //            .ForMember(dest => dest.exchange, opt => opt.MapFrom(src => src.Exchange))
    //            .ForMember(dest => dest.quantity_type, opt => opt.MapFrom(src => Convert.ToInt32(src.QuantityType)))
    //            .ForMember(dest => dest.price_type, opt => opt.MapFrom(src => Convert.ToInt32(src.PriceType)))
    //            .ForMember(dest => dest.qty, opt => opt.MapFrom(src => src.Qty))
    //            .ForMember(dest => dest.cash_qty, opt => opt.MapFrom(src => src.CashQty))
    //            .ForMember(dest => dest.percent, opt => opt.MapFrom(src => src.Percent))
    //            .ForMember(dest => dest.cum_qty, opt => opt.MapFrom(src => src.CumQty))
    //            .ForMember(dest => dest.leaves_qty, opt => opt.MapFrom(src => src.LeavesQty))
    //            .ForMember(dest => dest.avg_px, opt => opt.MapFrom(src => src.AvgPx))
    //            .ForMember(dest => dest.last_qty, opt => opt.MapFrom(src => src.LastQty))
    //            .ForMember(dest => dest.last_mkt, opt => opt.MapFrom(src => src.LastMkt))
    //            .ForMember(dest => dest.orders, opt => opt.MapFrom(src => src.Orders))
    //            .ForMember(dest => dest.execution_reports, opt => opt.MapFrom(src => src.ExecutionReports))
    //            ;
    //    }
    //}
}
