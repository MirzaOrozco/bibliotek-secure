using Application.Queries.Authors;
using Domain;
using Infrastructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Books
{
    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, OperationResult<List<Book>>>
    {
        private readonly FakeDatabase _db;

        public GetAllBooksQueryHandler(FakeDatabase db)
        {
            _db = db;
        }

        public async Task<OperationResult<List<Book>>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            var books = _db.Books;
            return OperationResult<List<Book>>.Successful(books, String.Format("Found {0} books", books.Count));
        }
    }
}
