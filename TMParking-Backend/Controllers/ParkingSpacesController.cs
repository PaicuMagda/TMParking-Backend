using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TMParking_Backend.Data;
using TMParking_Backend.Models;

namespace TMParking_Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ParkingSpacesController : ControllerBase
    {
        private readonly DbContextTMParking _dbContextTMParking;

        public ParkingSpacesController(DbContextTMParking dbContextTMParking)
        {
            _dbContextTMParking = dbContextTMParking;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllParkingSpaces()
        {
            var parkingSpaces = await _dbContextTMParking.ParkingSpaces
                .Include(p => p.ParkingSpacesOwner)
                .Include(s=>s.ParkingSpaceForOneParking).Select(p=> new 
                {
                    parkingSpacesId=p.ParkingSpacesId,
                    OwnerName = p.ParkingSpacesOwner.FullName ,
                    availableParkingSpaces=p.AvailableParkingSpaces,
                    name=p.Name,
                    startDate=p.StartDate,
                    endDate=p.EndDate,
                    isFree=p.IsFree,
                    isVideoSurveilance=p.IsVideoSurveilance,
                    isVerifiedByAdmin=p.IsVerifiedByAdmin,
                    isPersonalVehicleAccepted=p.IsPersonalVehicleAccepted,
                    isPublicTransportAccepted= p.IsPublicTransportAccepted,
                    isAgriculturalMachineryAccepted=p.IsAgriculturalMachineryAccepted,
                    isCargoVehicleAccepted=p.IsCargoVehicleAccepted,
                    imageProfile=p.ImageProfile,
                    ownerId=p.ParkingSpacesOwnerId,

                }
                
                ).ToListAsync();

            return Ok(parkingSpaces);
        }


        [HttpPost]
        public async Task<IActionResult> RegisterParkingSpaces([FromBody] ParkingSpaces parkingSpaces)
        {
            if (parkingSpaces == null)
                return BadRequest();

            await _dbContextTMParking.ParkingSpaces.AddAsync(parkingSpaces);
            await _dbContextTMParking.SaveChangesAsync();
            return Ok(new { Message = "Parking spaces successfully added !" });
        }


        [HttpGet("{userId}/parking-spaces")]
        public async Task<IActionResult> GetMyParkingSpaces(int userId)
        {
            var userParkingSpaces = await _dbContextTMParking.ParkingSpaces.Include(u=>u.ParkingSpacesOwner).Where(p=>p.ParkingSpacesOwnerId == userId).Select(
                p=> new 
                {
                    parkingSpacesId = p.ParkingSpacesId,
                    OwnerName = p.ParkingSpacesOwner.FullName,
                    availableParkingSpaces = p.AvailableParkingSpaces,
                    name = p.Name,
                    startDate = p.StartDate,
                    endDate = p.EndDate,
                    isFree = p.IsFree,
                    isVideoSurveilance = p.IsVideoSurveilance,
                    isVerifiedByAdmin = p.IsVerifiedByAdmin,
                    isPersonalVehicleAccepted = p.IsPersonalVehicleAccepted,
                    isPublicTransportAccepted = p.IsPublicTransportAccepted,
                    isAgriculturalMachineryAccepted = p.IsAgriculturalMachineryAccepted,
                    isCargoVehicleAccepted = p.IsCargoVehicleAccepted,
                    imageProfile = p.ImageProfile,
                    ownerId = p.ParkingSpacesOwnerId,
                    somethingIsWrong= p.SomethingIsWrong,

                    }
                ).ToListAsync();

            if (userParkingSpaces == null)
            {
                return NotFound();
            }

            return Ok(userParkingSpaces);
        
        }


        [HttpGet("{parkingSpacesId}/parkingSpaces")]
        public async Task<IActionResult> GetParkingSpacesById(int parkingSpacesId)
        { 
           var parkingSpaces = await _dbContextTMParking.ParkingSpaces.Include(x => x.ParkingSpacesOwner).
                Include(p => p.ParkingSpaceForOneParking).Select(p => new 
                {
                    parkingSpacesId=p.ParkingSpacesId,
                    addedDate= p.AddedDate,
                    address=p.Address,
                    availableParkingSpaces=p.AvailableParkingSpaces,
                    description=p.Description,
                    endDate=p.EndDate,
                    imageProfile=p.ImageProfile,
                    isAgriculturalMachineryAccepted=p.IsAgriculturalMachineryAccepted,
                    isCargoVehicleAccepted=p.IsCargoVehicleAccepted,
                    isDraft=p.IsDraft,
                    isFree=p.IsFree,
                    isPersonalVehicleAccepted=p.IsPersonalVehicleAccepted,
                    isPublicTransportAccepted=p.IsPublicTransportAccepted,
                    isVerifiedByAdmin=p.IsVerifiedByAdmin,
                    isVideoSurveilance=p.IsVideoSurveilance,
                    leasePermit=p.LeasePermit,
                    multistoreyCarPark=p.MultistoreyCarPark,
                    name=p.Name,
                    paidParking=p.PaidParking,
                    parkingSpacesOwnerId=p.ParkingSpacesOwnerId,
                    paymentForSubscription=p.PaymentForSubscription,
                    paymentPerDay=p.PaymentPerDay,
                    paymentPerHour=p.PaymentPerHour,
                    somethingIsWrong=p.SomethingIsWrong,
                    startDate=p.StartDate,
                    undergroundParkingLots=p.UndergroundParkingLots,
                    ownerName=p.ParkingSpacesOwner.FullName
                }).FirstOrDefaultAsync(p=>p.parkingSpacesId == parkingSpacesId);

            if (parkingSpaces == null)
            {
                return NotFound();
            }
        return Ok(parkingSpaces);
        }


        [HttpDelete("{parkingSpacesId}")]
        public async Task<IActionResult> DeleteParkingSpaces(int parkingSpacesId)
        {
            ParkingSpaces parkingSpaces = await _dbContextTMParking.ParkingSpaces.FirstOrDefaultAsync(p => p.ParkingSpacesId == parkingSpacesId);

            if (parkingSpaces == null)
            {
                return NotFound();
            }

            _dbContextTMParking.ParkingSpaces.Remove(parkingSpaces);
            _dbContextTMParking.SaveChanges();
            return Ok(new { Message = "Parking Spaces was successfully deleted !"});
        }


        [HttpPut("update-parking-spaces/{id}")]
        public async Task<IActionResult> UpdateParkingSpaces(int id, [FromBody] ParkingSpaces parkingSpaces)
        {
            var parkingSpacesToUpdate = await _dbContextTMParking.ParkingSpaces.FirstOrDefaultAsync(p => p.ParkingSpacesId == id);

            if (parkingSpacesToUpdate == null)
            {
                return NotFound("Parking Spaces not found !");
            }

            parkingSpacesToUpdate.Name = parkingSpaces.Name;
            parkingSpacesToUpdate.Address=parkingSpaces.Address;
            parkingSpacesToUpdate.AvailableParkingSpaces = parkingSpaces.AvailableParkingSpaces;
            parkingSpacesToUpdate.IsCargoVehicleAccepted =parkingSpaces.IsCargoVehicleAccepted;
            parkingSpacesToUpdate.IsPersonalVehicleAccepted=parkingSpaces.IsPersonalVehicleAccepted;
            parkingSpacesToUpdate.IsPublicTransportAccepted=parkingSpaces.IsPublicTransportAccepted;
            parkingSpacesToUpdate.IsAgriculturalMachineryAccepted=parkingSpaces.IsAgriculturalMachineryAccepted;    
            parkingSpacesToUpdate.ImageProfile=parkingSpaces.ImageProfile;
            parkingSpacesToUpdate.LeasePermit=parkingSpaces.LeasePermit;
            parkingSpacesToUpdate.StartDate = parkingSpaces.StartDate;
            parkingSpacesToUpdate.EndDate = parkingSpaces.EndDate;
            parkingSpacesToUpdate.AddedDate = parkingSpaces.AddedDate;
            parkingSpacesToUpdate.IsFree= parkingSpaces.IsFree;
            parkingSpacesToUpdate.IsVideoSurveilance=parkingSpaces.IsVideoSurveilance;
            parkingSpacesToUpdate.Description=parkingSpaces.Description;
            parkingSpacesToUpdate.IsDraft=parkingSpaces.IsDraft;
            parkingSpacesToUpdate.PaymentPerHour = parkingSpaces.PaymentPerHour;
            parkingSpacesToUpdate.PaymentPerDay=parkingSpaces.PaymentPerDay;
            parkingSpacesToUpdate.PaymentForSubscription=parkingSpaces.PaymentForSubscription;
            parkingSpacesToUpdate.IsVerifiedByAdmin = parkingSpaces.IsVerifiedByAdmin;
            parkingSpacesToUpdate.SomethingIsWrong=parkingSpaces.SomethingIsWrong;
            parkingSpacesToUpdate.UndergroundParkingLots = parkingSpaces.UndergroundParkingLots;
            parkingSpacesToUpdate.MultistoreyCarPark = parkingSpaces.MultistoreyCarPark;
            parkingSpacesToUpdate.PaidParking=parkingSpaces.PaidParking;
            

            await _dbContextTMParking.SaveChangesAsync();

            return Ok(new { Message = "Parking Spaces update successfully !" });
        }
    }
}
