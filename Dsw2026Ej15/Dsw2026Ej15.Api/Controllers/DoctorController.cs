using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Domain;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Dsw2026Ej15.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly IPersistance _persistence;

        public DoctorController(IPersistance persistence)
        {
            _persistence = persistence;
        }

        [HttpPost]
        public async Task<ActionResult> CreateDoctor([FromBody] DoctorModel.Request request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                return BadRequest("Name es requerido.");

            if (string.IsNullOrWhiteSpace(request.LicenseNumber))
                return BadRequest("LicenseNumber es requerido.");
            var speciality = await _persistence.GetSpecialityByIdAsync(request.SpecialityId);
            if (speciality == null)
                return BadRequest("La especialidad indicada no existe.");

            var newDoctor = new Doctor
            {
                _id = Guid.NewGuid(),
                _name = request.Name,
                _licenseNumber = request.LicenseNumber,
                _speciality = speciality,
                _isActive = true
            };

            await _persistence.AddDoctorAsync(newDoctor);

            return Created();
        }

        [HttpGet]
        public async Task<IActionResult> GetActiveDoctors()
        {
            var doctors = await _persistence.GetActiveDoctorsAsync();
            return Ok(doctors);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetDoctorById(Guid id)
        {
            var doctor = await _persistence.GetActiveDoctorByIdAsync(id);

            if (doctor == null)
                throw new NotFoundException($"El médico con id {id} no existe o no está activo.");

            var response = new DoctorModel.Response(doctor._name, doctor._licenseNumber, doctor._speciality._name);
            return Ok(response);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteDoctor(Guid id)
        {
            var doctor = await _persistence.GetActiveDoctorByIdAsync(id);

            if (doctor == null)
                throw new NotFoundException($"El médico con id {id} no existe o no está activo.");

            doctor._isActive = false;
            await _persistence.DeactivateDoctorAsync(id);

            return NoContent();
        }
    }
}
