using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fwk.Main.BusinessEntities.Positions;
using Fwk.Main.DataAccess;

namespace Fwk.Main.DataAccessLayer.Profiles
{
    //public class ExecutionSummaryProfile : Profile
    //{
    //    public override string ProfileName
    //    {
    //        get { return "ExecutionSummaryProfile"; }
    //    }
    //    protected override void Configure()
    //    {
    //        CreateMap<execution_summaries, ExecutionSummary>()
    //            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.id))
    //            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.date))
    //            .ForMember(dest => dest.Symbol, opt => opt.MapFrom(src => src.symbol))
    //            .ForMember(dest => dest.AvgPx, opt => opt.MapFrom(src => src.avg_px))
    //            .ForMember(dest => dest.CumQty, opt => opt.MapFrom(src => src.cum_qty))
    //            .ForMember(dest => dest.LeavesQty, opt => opt.MapFrom(src => src.leaves_qty))
    //            .ForMember(dest => dest.Commission, opt => opt.MapFrom(src => src.commission))
    //            .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.text))
    //            .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.positions))
              
    //            ;

    //        CreateMap<ExecutionSummary, execution_summaries>()
    //            .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id))
    //            .ForMember(dest => dest.date, opt => opt.MapFrom(src => src.Date))
    //            .ForMember(dest => dest.symbol, opt => opt.MapFrom(src => src.Symbol))
    //            .ForMember(dest => dest.avg_px, opt => opt.MapFrom(src => src.AvgPx))
    //            .ForMember(dest => dest.cum_qty, opt => opt.MapFrom(src => src.CumQty))
    //            .ForMember(dest => dest.leaves_qty, opt => opt.MapFrom(src => src.LeavesQty))
    //            .ForMember(dest => dest.commission, opt => opt.MapFrom(src => src.Commission))
    //            .ForMember(dest => dest.text, opt => opt.MapFrom(src => src.Text))
    //            .ForMember(dest => dest.positions, opt => opt.MapFrom(src => src.Position))
    //            ;
    //    }
    //}
}
