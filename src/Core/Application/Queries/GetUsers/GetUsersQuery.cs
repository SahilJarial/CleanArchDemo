using AutoMapper;
using Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Queries.GetUsers
{

    public sealed record GetUsersQuery : IRequest<List<UserDto>>;

    public sealed class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<UserDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetUsersQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _context.Users.ToListAsync(cancellationToken);
            return _mapper.Map<List<UserDto>>(users);
        }
    }

    public sealed record UserDto
    {
        public Guid Id { get; init; }
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public DateTime CreatedAt { get; init; }
    }
}
