using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyTry.Services.CouponAPI.Data;
using MyTry.Services.CouponAPI.Model.Dto;
using System.Collections.Generic;

namespace MyTry.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private ResponseDto _response;
        

        public CouponAPIController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _response = new ResponseDto();
        }


        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Coupon> objList = _context.Coupons.ToList();
                _response.Result = _mapper.Map<IEnumerable<CouponDto>>(objList);
            }
            catch(Exception ex) 
            {
                _response.IsSuccess = false;
                 _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                 Coupon obj = _context.Coupons.First(u=>u.CouponId == id);
                _response.Result = _mapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpGet]
        [Route("GetByCode/{code}")]
        public ResponseDto GetByCode(string code)
        {
            try
            {
                Coupon obj = _context.Coupons.First(u => u.CouponCode.ToLower() == code.ToLower());

                _response.Result = _mapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpPost]         
        public ResponseDto Post([FromBody] CouponDto couponDto)
        {
            try
            {
                 Coupon obj = _mapper.Map<Coupon>(couponDto);
                _context.Coupons.Add(obj);
                _context.SaveChanges();
                _response.Result = _mapper.Map<CouponDto>(obj);
                 
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpPut]
        public ResponseDto Put([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(couponDto);
                _context.Coupons.Update(obj);
                _context.SaveChanges();
                _response.Result = _mapper.Map<CouponDto>(obj);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpDelete]
        public ResponseDto Delete(int id)
        {
            try
            {
                Coupon obj = _context.Coupons.First(u => u.CouponId == id);
                _context.Coupons.Remove(obj);
                _context.SaveChanges();
                 

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

    }
}
