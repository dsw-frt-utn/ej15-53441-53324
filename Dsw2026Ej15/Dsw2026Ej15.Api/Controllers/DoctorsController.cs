using Microsoft.AspNetCore.Mvc;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain;

namespace Dsw2026Ej15.Api.Controllers
{
	[ApiController]
	[Route("api/doctors")]
	public class DoctorsController : ControllerBase
	{
		private readonly IPersistence _persistence;

		// El constructor recibe la persistencia en memoria de tu amiga
		public DoctorsController(IPersistence persistence)
		{
			_persistence = persistence;
		}

		// 1. PRIMER ENDPOINT: POST api/doctors (Agregar Médico)
		[HttpPost]
		public IActionResult CreateDoctor([FromBody] DoctorRequest request)
		{
			if (string.IsNullOrWhiteSpace(request.Name))
			{
				return BadRequest("Name es requerido.");
			}

			if (string.IsNullOrWhiteSpace(request.LicenseNumber))
			{
				return BadRequest("LicenseNumber es requerido.");
			}

			var specialities = _persistence.GetSpecialities();
			var specialityExists = specialities.Any(s => s.Id == request.SpecialityId);

			if (!specialityExists)
			{
				return BadRequest("SpecialityId debe existir.");
			}

			var newDoctor = new Doctor
			{
				Id = Guid.NewGuid(),
				Name = request.Name,
				LicenseNumber = request.LicenseNumber,
				IsActive = true
			};

			_persistence.SaveDoctor(newDoctor);

			return Created($"api/doctors/{newDoctor.Id}", newDoctor);
		}

		// 2. SEGUNDO ENDPOINT: GET api/doctors (Listar todos)
		[HttpGet]
		public IActionResult GetAllDoctors()
		{
			var doctors = _persistence.GetDoctors();
			return Ok(doctors);
		}

		// 3. TERCER ENDPOINT: GET api/doctors/{id} (Buscar por ID)
		[HttpGet("{id}")]
		public IActionResult GetDoctorById(Guid id)
		{
			var doctor = _persistence.GetDoctors().FirstOrDefault(d => d.Id == id);

			if (doctor == null || !doctor.IsActive)
			{
				return NotFound("El médico no existe o está inactivo.");
			}

			var speciality = _persistence.GetSpecialities().FirstOrDefault(s => s.Id == doctor.SpecialityId);
			var specialityName = speciality != null ? speciality.Name : "Desconocida";

			var response = new
			{
				doctor.Name,
				doctor.LicenseNumber,
				SpecialityName = specialityName
			};

			return Ok(response);
		}

		// 4. CUARTO ENDPOINT: DELETE api/doctors/{id} (Dar de baja / Inactivar)
		[HttpDelete("{id}")]
		public IActionResult DeleteDoctor(Guid id)
		{
			var doctor = _persistence.GetDoctors().FirstOrDefault(d => d.Id == id);

			if (doctor == null)
			{
				return NotFound("Médico no encontrado.");
			}

			doctor.IsActive = false;

			return NoContent();
		}
	}

	// Esta clase ayuda a recibir el JSON que nos manden
	public class DoctorRequest
	{
		public string Name { get; set; }
		public string LicenseNumber { get; set; }
		public Guid SpecialityId { get; set; }
	}
} 