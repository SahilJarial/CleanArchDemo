using Core.Application.Common.Security;
using Core.Application.Interfaces;
using Core.Domain.Entities;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Commands
{



    [Authentication("Staff")]
    public sealed record CreateUserCommand : IRequest<Guid>
    {
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
    }

    public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
        }
    }

    public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public CreateUserCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
