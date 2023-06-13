using Application.DTOs.Role;
using Application.Interfaces;
using Application.ResponseModel;
using Domain.Entities.IdentityEntities;
using HotelManagement1.Filters;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoleController : ApiControllerBase<Role>
{
    private readonly IRoleRepository _roleRepository;
    private readonly IPermissionRepository _permissionRepository;
    public RoleController(IRoleRepository roleRepository, IPermissionRepository permissionRepository)
    {
        _roleRepository = roleRepository;
        _permissionRepository = permissionRepository;
    }
    [HttpGet("[action]")]
    //[Authorize(Roles = "GetAllRole")]
    public async Task<ActionResult<ResponseCore<IEnumerable<RoleGetDTO>>>> GetAll()
    {
        IEnumerable<Role> roles = await _roleRepository.GetAsync(x => true);

        IEnumerable<RoleGetDTO> mappedRoles = _mapper.Map<IEnumerable<RoleGetDTO>>(roles);

        return Ok(new ResponseCore<IEnumerable<RoleGetDTO>>(mappedRoles));
    }

    [HttpGet("[action]")]
    //[Authorize(Roles = "GetByIdRole")]
    public async Task<ActionResult<ResponseCore<RoleGetDTO>>> GetById(Guid id)
    {
        Role? obj = await _roleRepository.GetByIdAsync(id);
        if (obj == null)
        {
            return NotFound(new ResponseCore<Role?>(false, id + " not found!"));
        }
        RoleGetDTO mappedRole = _mapper.Map<RoleGetDTO>(obj);
        return Ok(new ResponseCore<RoleGetDTO?>(mappedRole));
    }

    [HttpPut("[action]")]
    [ActionModelValidation]
    //[Authorize(Roles = "UpdateRole")]
    public async Task<ActionResult<ResponseCore<RoleGetDTO>>> Update([FromBody] RoleUpdateDTO role)
    {
        Role? mappedRole = _mapper.Map<Role>(role);
        var validationResult = _validator.Validate(mappedRole);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ResponseCore<Role>(false, validationResult.Errors));
        }
        mappedRole.Permissions = new List<Permission>();
        foreach (var item in role.Permissions)
        {

            Permission? permission = await _permissionRepository.GetByIdAsync(item);
            if (permission != null)
                mappedRole.Permissions.Add(permission);
            else return BadRequest(new ResponseCore<Role>(false, item + " Id not found"));
        }
        mappedRole = await _roleRepository.UpdateAsync(mappedRole);
        if (mappedRole != null)
            return Ok(new ResponseCore<RoleGetDTO>(_mapper.Map<RoleGetDTO>(mappedRole)));
        return BadRequest(new ResponseCore<Role>(false, role + " not found"));

    }

    [HttpPost("[action]")]
    [ActionModelValidation]
    //[Authorize(Roles = "CreateRole")]
    public async Task<ActionResult<ResponseCore<RoleGetDTO>>> Create([FromBody] RoleCreateDTO role)
    {
        Role mappedRole = _mapper.Map<Role>(role);
        var validationResult = _validator.Validate(mappedRole);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ResponseCore<object>(false, validationResult.Errors));
        }
        mappedRole.Permissions = new List<Permission>();
        foreach (Guid item in role.Permissions)
        {
            Permission? permission = await _permissionRepository.GetByIdAsync(item);
            if (permission != null)
                mappedRole.Permissions.Add(permission);
            else return BadRequest(new ResponseCore<string>(false, item + " Id not found"));
        }
        mappedRole = await _roleRepository.CreateAsync(mappedRole);
        RoleGetDTO roleGetDTO = new()
        {
            Name = mappedRole.Name,
            Permissions = mappedRole.Permissions.Select(x => x.Id).ToList(),
            RoleId = mappedRole.Id
        };
        return Ok(new ResponseCore<object>(roleGetDTO));
    }

    [HttpDelete("[action]")]
    //[Authorize(Roles = "DeleteRole")]
    public async Task<ActionResult<ResponseCore<bool>>> Delete(Guid id)
    {
        return await _roleRepository.DeleteAsync(id) ?
            Ok(new ResponseCore<bool>(true))
          : BadRequest(new ResponseCore<bool>(false, "Delete failed!"));

    }

}
