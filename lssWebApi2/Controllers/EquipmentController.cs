using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2.EquipmentDomain;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class EquipmentController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(EquipmentView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddEquipment([FromBody]EquipmentView view)
        {
            EquipmentModule invMod = new EquipmentModule();

            NextNumber nnEquipment = await invMod.Equipment.Query().GetNextNumber();

            view.EquipmentNumber = nnEquipment.NextNumberValue;

            Equipment equipment = await invMod.Equipment.Query().MapToEntity(view);

            invMod.Equipment.AddEquipment(equipment).Apply();

            EquipmentView newView = await invMod.Equipment.Query().GetViewByNumber(view.EquipmentNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(EquipmentView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteEquipment([FromBody]EquipmentView view)
        {
            EquipmentModule invMod = new EquipmentModule();
            Equipment equipment = await invMod.Equipment.Query().MapToEntity(view);
            invMod.Equipment.DeleteEquipment(equipment).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(EquipmentView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateEquipment([FromBody]EquipmentView view)
        {
            EquipmentModule invMod = new EquipmentModule();

            Equipment equipment = await invMod.Equipment.Query().MapToEntity(view);


            invMod.Equipment.UpdateEquipment(equipment).Apply();

            EquipmentView retView = await invMod.Equipment.Query().GetViewById(equipment.EquipmentId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{EquipmentId}")]
        [ProducesResponseType(typeof(EquipmentView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetEquipmentView(long equipmentId)
        {
            EquipmentModule invMod = new EquipmentModule();

            EquipmentView view = await invMod.Equipment.Query().GetViewById(equipmentId);
            return Ok(view);
        }
        }
	}
        