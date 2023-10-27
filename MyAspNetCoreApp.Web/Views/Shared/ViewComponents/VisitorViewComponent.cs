using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyAspNetCoreApp.Web.Models;
using MyAspNetCoreApp.Web.ViewModel;

namespace MyAspNetCoreApp.Web.Views.Shared.ViewComponents
{
    public class VisitorViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public VisitorViewComponent(AppDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var visitor = _context.Visitors.ToList();
            var visitorViewModels = _mapper.Map<List<VisitorViewModel>>(visitor);
            ViewBag.visitorViewModels = visitorViewModels;
            return View();
        }
    }
}
