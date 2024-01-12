using Microsoft.AspNetCore.Mvc;
using MyTry.Web.Models;
using MyTry.Web.Service.IService;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MyTry.Web.Controllers
{
    public class CouponController : Controller
    {

        private readonly ICouponService _couponService;
        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }




        public async Task <IActionResult> CouponIndex()
        {
            List<CouponDto>? list = new();

            ResponseDto? response = await _couponService.GetAllCouponsAsync();

            if (response != null && response.IsSuccess)
            {
                try
                {
                    list = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return View(list);
        }


        public async Task<IActionResult> CouponCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CouponCreate(CouponDto model)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _couponService.CreateCouponAsync(model);

                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(CouponIndex));
                }
            }
            return View();
        }


    }



}
