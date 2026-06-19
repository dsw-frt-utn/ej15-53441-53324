using Microsoft.AspNetCore.Mvc;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dsw2026Ej15.Api.Controllers
{
	[ApiController]
	[Route("api/doctors")]
	public class DoctorsController : ControllerBase
	{
		private readonly IPersistance _persistence;

		public DoctorsController(IPersistance persistence)
		{
			_persistence = persistence;
		}

		[HttpPost]
		public async Task<IActionResult> CreateDoctor([FromBody] DoctorModel.Request request)
		{
			if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.LicenseNumber))
			{
				throw new ValidationException("Nombre y matricula son requeridos.");
			}

			var specialities = _persistence.GetSpecialities();
			var speciality = specialities.SingleOrDefault(s => s.Id == request.SpecialityId);

			if (speciality == null)
			{
				throw new ValidationException("La especialidad especificada no existe.");
			}

			var newDoctor = new Doctor(request.Name, request.LicenseNumber, speciality);

			_persistence.SaveDoctor(newDoctor);

			return Created($"api/doctors/{newDoctor.Id}", newDoctor);
		}

		[HttpGet]
		public async Task<IActionResult> GetAllDoctors()
		{
			var activeDoctors = _persistence.GetDoctors().Where(d => d.IsActive).ToList();
			return Ok(activeDoctors);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetDoctorById(Guid id)
		{
			var doctor = _persistence.GetDoctors().FirstOrDefault(d => d.Id == id);

			if (doctor == null || !doctor.IsActive)
			{
				throw new NotFoundException("El médico no existe o está inactivo.");
			}

			var response = new DoctorModel.Response(doctor.Name, doctor.LicenseNumber, doctor.Speciality.Name);

			return Ok(response);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteDoctor(Guid id)
		{
			var doctor = _persistence.GetDoctors().FirstOrDefault(d => d.Id == id);

			if (doctor == null || !doctor.IsActive)
			{
				throw new NotFoundException("El médico no se encuentra activo o no existe.");
			}

			doctor.IsActive = false;

			return NoContent();
		}
	}

	public record DoctorModel
	{
		public record Request(string Name, string LicenseNumber, Guid SpecialityId);
		public record Response(string Name, string LicenseNumber, string SpecialityName);
	}
}
