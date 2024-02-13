using AutoMapper;
using coe.dnd.dal.Contexts;
using coe.dnd.dal.Models;
using coe.dnd.dal.Specifications.Players;
using coe.dnd.services.DataTransferObjects;
using coe.dnd.services.Interfaces;
using BC = BCrypt.Net.BCrypt;
using Unosquare.EntityFramework.Specification.Common.Extensions;

namespace coe.dnd.services.Services;

public class AuthenticationService: IAuthenticationService
{
    private readonly DndOrganiserContext _database;
    private readonly IMapper _mapper;

    public AuthenticationService(DndOrganiserContext database, IMapper mapper)
    {
        _database = database;
        _mapper = mapper;
    }

    public PlayerDto Authenticate(string email, string password)
    {
        var player = _database.Get<Player>().Where(new PlayerByEmailSpec(email)).SingleOrDefault();
        
        if (player == null || !BC.Verify(password, player.Password)) return null;

        return _mapper.Map<PlayerDto>(player);
    }

    
}