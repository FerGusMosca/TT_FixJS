using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fwk.Main.BusinessEntities.Orders;
using Fwk.Main.Common.Enums;
using Fwk.Main.DataAccess;

namespace Fwk.Main.DataAccessLayer.Profiles
{
    //public class OrderProfile : Profile
    //{
    //    public override string ProfileName
    //    {
    //        get { return "OrderProfile"; }
    //    }

    //    protected override void Configure()
    //    {
    //        CreateMap<orders, Order>()
    //            .ForMember(dest => dest.ClOrdId, opt => opt.MapFrom(src => src.clord_id))
    //            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.order_id))
    //            .ForMember(dest => dest.Symbol, opt => opt.MapFrom(src => src.symbol))
    //            .ForMember(dest => dest.SettlType, opt => opt.MapFrom(src => (SettlType) Convert.ToChar(src.settl_type)))
    //            .ForMember(dest => dest.SettlDate, opt => opt.MapFrom(src => src.settl_date))
    //            .ForMember(dest => dest.Side, opt => opt.MapFrom(src => (Side)Convert.ToChar(src.side)))
    //            .ForMember(dest => dest.Exchange, opt => opt.MapFrom(src => src.exchange))
    //            .ForMember(dest => dest.OrdType, opt => opt.MapFrom(src => (OrdType) Convert.ToChar(src.ord_type)))
    //            .ForMember(dest => dest.QuantityType, opt => opt.MapFrom(src => (QuantityType)Convert.ToChar(src.quantity_type)))
    //            .ForMember(dest => dest.OrderQty, opt => opt.MapFrom(src => src.order_qty))
    //            .ForMember(dest => dest.CashOrderQty, opt => opt.MapFrom(src => src.cash_order_qty))
    //            .ForMember(dest => dest.OrderPercent, opt => opt.MapFrom(src => src.order_percent))
    //            .ForMember(dest => dest.PriceType, opt => opt.MapFrom(src => (PriceType)src.price_type))
    //            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.price))
    //            .ForMember(dest => dest.StopPx, opt => opt.MapFrom(src => src.stop_px))
    //            .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.currency))
    //            .ForMember(dest => dest.TimeInForce, opt => opt.MapFrom(src => (TimeInForce?) src.time_inforce))
    //            .ForMember(dest => dest.ExpireTime, opt => opt.MapFrom(src => src.expire_time))
    //            .ForMember(dest => dest.EffectiveTime, opt => opt.MapFrom(src => src.effective_time))
    //            .ForMember(dest => dest.MinQty, opt => opt.MapFrom(src => src.min_qty))
    //            .ForMember(dest => dest.Index, opt => opt.MapFrom(src => src.index))
    //            ;

    //        CreateMap<Order, orders>()
    //            .ForMember(dest => dest.clord_id, opt => opt.MapFrom(src => src.ClOrdId))
    //            .ForMember(dest => dest.order_id, opt => opt.MapFrom(src => src.OrderId))
    //            .ForMember(dest => dest.symbol, opt => opt.MapFrom(src => src.Symbol))
    //            .ForMember(dest => dest.settl_type, opt => opt.MapFrom(src => Convert.ToChar(src.SettlType).ToString()))
    //            .ForMember(dest => dest.settl_date, opt => opt.MapFrom(src => src.SettlDate))
    //            .ForMember(dest => dest.side, opt => opt.MapFrom(src => Convert.ToChar(src.Side).ToString()))
    //            .ForMember(dest => dest.exchange, opt => opt.MapFrom(src => src.Exchange))
    //            .ForMember(dest => dest.ord_type, opt => opt.MapFrom(src => src.OrdType != null ? Convert.ToChar(src.OrdType).ToString() : null))
    //            .ForMember(dest => dest.quantity_type, opt => opt.MapFrom(src => Convert.ToInt32(src.QuantityType)))
    //            .ForMember(dest => dest.cash_order_qty, opt => opt.MapFrom(src => src.OrderQty))
    //            .ForMember(dest => dest.cash_order_qty, opt => opt.MapFrom(src => src.CashOrderQty))
    //            .ForMember(dest => dest.order_percent, opt => opt.MapFrom(src => src.OrderPercent))
    //            .ForMember(dest => dest.price_type, opt => opt.MapFrom(src => Convert.ToInt32(src.PriceType)))
    //            .ForMember(dest => dest.price, opt => opt.MapFrom(src => src.Price))
    //            .ForMember(dest => dest.stop_px, opt => opt.MapFrom(src => src.StopPx))
    //            .ForMember(dest => dest.currency, opt => opt.MapFrom(src => src.Currency))
    //            .ForMember(dest => dest.time_inforce, opt => opt.MapFrom(src => src.TimeInForce != null ? (int?)Convert.ToInt32(src.TimeInForce.Value) : null))
    //            .ForMember(dest => dest.expire_time, opt => opt.MapFrom(src => src.ExpireTime))
    //            .ForMember(dest => dest.effective_time, opt => opt.MapFrom(src => src.EffectiveTime))
    //            .ForMember(dest => dest.min_qty, opt => opt.MapFrom(src => src.MinQty))
    //            .ForMember(dest => dest.index, opt => opt.MapFrom(src => src.Index))
    //            ;
    //    }
    //}
}
