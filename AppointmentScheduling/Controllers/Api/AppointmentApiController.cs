﻿using AppointmentScheduling.Models.ViewModels;
using AppointmentScheduling.Services;
using AppointmentScheduling.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AppointmentScheduling.Controllers.Api
{
    [Route("api/Appointment")]
    [ApiController]
    public class AppointmentApiController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string loginUserId;
        private readonly string role;

        public AppointmentApiController(IAppointmentService appointmentService, IHttpContextAccessor httpContextAccessor)
        {
            _appointmentService = appointmentService;
            _httpContextAccessor = httpContextAccessor;
            loginUserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            role = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
        }

        [HttpPost]
        [Route("SaveCalendarData")]
        public IActionResult SaveCalendarData(AppointmentViewModel data)
        {
            CommonResponse<int> commonResponse = new CommonResponse<int>();
            try
            {
                commonResponse.status = _appointmentService.AddUpdate(data).Result;

                if (commonResponse.status == 1)
                {
                    commonResponse.message = Helper.appointmentUpdated;
                }
                if (commonResponse.status == 2)
                {
                    commonResponse.message = Helper.appointmentAdded;
                }
            }
            catch (Exception ex)
            {
                commonResponse.message = ex.Message;
                commonResponse.status = Helper.failure_code;
            }

            return Ok(commonResponse);
        }

        [HttpGet]
        [Route("GetCalendarData")]
        public IActionResult GetCalendarData(string doctorId)
        {
            CommonResponse<List<AppointmentViewModel>> commonResponse = new CommonResponse<List<AppointmentViewModel>>();
            try
            {
                if (role == Helper.Patient)
                {
                    commonResponse.dataEnum = _appointmentService.PatientsEventsById(loginUserId);
                    commonResponse.status = Helper.success_code;
                }
                else if (role == Helper.Doctor)
                {
                    commonResponse.dataEnum = _appointmentService.DoctorsEventsById(loginUserId);
                    commonResponse.status = Helper.success_code;
                }
                else
                {
                    commonResponse.dataEnum = _appointmentService.DoctorsEventsById(doctorId);
                    commonResponse.status = Helper.success_code;
                }
            }
            catch (Exception ex)
            {
                commonResponse.message = ex.Message;
                commonResponse.status = Helper.failure_code;
            }
            return Ok(commonResponse);
        }

        [HttpGet]
        [Route("GetCalendarDataById/{id}")]
        public IActionResult GetCalendarDataById(int id)
        {
            CommonResponse<AppointmentViewModel> commonResponse = new CommonResponse<AppointmentViewModel>();
            try
            {
                commonResponse.dataEnum = _appointmentService.GetById(id);
                commonResponse.status = Helper.success_code;
            }
            catch (Exception ex)
            {
                commonResponse.message = ex.Message;
                commonResponse.status = Helper.failure_code;
            }
            return Ok(commonResponse);
        }

        [HttpGet]
        [Route("DeleteAppointment/{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            CommonResponse<int> commonResponse = new CommonResponse<int>();
            try
            {
                commonResponse.status = await _appointmentService.Delete(id);
                commonResponse.message = commonResponse.status == 1 ? Helper.appointmentDeleted : Helper.somethingWentWrong;
            }
            catch (Exception ex)
            {
                commonResponse.message = ex.Message;
                commonResponse.status = Helper.failure_code;
            }
            return Ok(commonResponse);
        }

        [HttpGet]
        [Route("ConfirmEvent/{id}")]
        public IActionResult ConfirmEvent(int id)
        {
            CommonResponse<int> commonResponse = new CommonResponse<int>();
            try
            {
                var result = _appointmentService.ConfirmEvent(id).Result;

                if (result > 0)
                {
                    commonResponse.status = Helper.success_code;
                    commonResponse.message = Helper.meetingConfirm;
                }
                else
                {
                    commonResponse.status = Helper.failure_code;
                    commonResponse.message = Helper.meetingConfirmError;
                }
            }
            catch (Exception ex)
            {
                commonResponse.message = ex.Message;
                commonResponse.status = Helper.failure_code;
            }
            return Ok(commonResponse);
        }
    }
}