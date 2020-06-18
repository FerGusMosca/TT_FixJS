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
    //public class ExecutionReportProfile : Profile
    //{
    //    public override string ProfileName
    //    {
    //        get { return "ExecutionReportProfile"; }
    //    }

    //    protected override void Configure()
    //    {
    //        CreateMap<execution_reports, ExecutionReport>()
    //            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
    //            .ForMember(dest => dest.ExecID, opt => opt.MapFrom(src => src.exec_id))
    //            .ForMember(dest => dest.TransactTime, opt => opt.MapFrom(src => src.transact_time))
    //            .ForMember(dest => dest.ExecType, opt => opt.MapFrom(src => (ExecType)Convert.ToChar(src.exec_type)))
    //            .ForMember(dest => dest.OrdStatus, opt => opt.MapFrom(src => (OrdStatus)Convert.ToChar(src.ord_status)))
    //            .ForMember(dest => dest.OrdRejReason, opt => opt.MapFrom(src => src.ord_rej_reason != null ? (OrdRejReason?)Convert.ToChar(src.ord_rej_reason) : null))
    //            .ForMember(dest => dest.LastQty, opt => opt.MapFrom(src => src.last_qty))
    //            .ForMember(dest => dest.LastPx, opt => opt.MapFrom(src => src.last_px))
    //            .ForMember(dest => dest.LastMkt, opt => opt.MapFrom(src => src.last_mkt))
    //            .ForMember(dest => dest.LeavesQty, opt => opt.MapFrom(src => src.leaves_qty))
    //            .ForMember(dest => dest.CumQty, opt => opt.MapFrom(src => src.cum_qty))
    //            .ForMember(dest => dest.AvgPx, opt => opt.MapFrom(src => src.avg_px))
    //            .ForMember(dest => dest.Commission, opt => opt.MapFrom(src => src.commission))
    //            .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.text))
    //            ;

    //        CreateMap<ExecutionReport, execution_reports>()
    //            .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id))
    //            .ForMember(dest => dest.exec_id, opt => opt.MapFrom(src => src.ExecID))
    //            .ForMember(dest => dest.transact_time, opt => opt.MapFrom(src => src.TransactTime))
    //            .ForMember(dest => dest.exec_type, opt => opt.MapFrom(src => Convert.ToChar(src.ExecType).ToString()))
    //            .ForMember(dest => dest.ord_status, opt => opt.MapFrom(src => Convert.ToChar(src.OrdStatus).ToString()))
    //            .ForMember(dest => dest.ord_rej_reason, opt => opt.MapFrom(src => src.OrdRejReason != null ? (int?)Convert.ToInt32(src.OrdRejReason) : null))
    //            .ForMember(dest => dest.last_qty, opt => opt.MapFrom(src => src.LastQty))
    //            .ForMember(dest => dest.last_px, opt => opt.MapFrom(src => src.LastPx))
    //            .ForMember(dest => dest.last_mkt, opt => opt.MapFrom(src => src.LastMkt))
    //            .ForMember(dest => dest.leaves_qty, opt => opt.MapFrom(src => src.LeavesQty))
    //            .ForMember(dest => dest.cum_qty, opt => opt.MapFrom(src => src.CumQty))
    //            .ForMember(dest => dest.avg_px, opt => opt.MapFrom(src => src.AvgPx))
    //            .ForMember(dest => dest.commission, opt => opt.MapFrom(src => src.Commission))
    //            .ForMember(dest => dest.text, opt => opt.MapFrom(src => src.Text))
    //            ;
    //    }
    //}
}
