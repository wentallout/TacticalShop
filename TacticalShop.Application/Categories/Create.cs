using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TacticalShop.Application.Core;
using TacticalShop.Domain;
using TacticalShop.Persistence;
using TacticalShop.ViewModels;

namespace TacticalShop.Application.Categories
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public CategoryCreateRequest categoryCreateRequest { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.categoryCreateRequest).SetValidator(new CategoryValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DatabaseContext _context;

            public Handler(DatabaseContext context)
            {
                _context = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var category = new Category
                {
                    CategoryName = request.categoryCreateRequest.CategoryName,
                    CategoryDescription = request.categoryCreateRequest.CategoryDescription,
                };

                _context.Categories.Add(category);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to create category");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}