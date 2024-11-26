using Infrastructure.Data;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Commands.Authors;

namespace API.Handler.Authors
{
    public class CreateAuthorCommandHandler
    {
        private readonly FakeDatabase _db;

        public CreateAuthorCommandHandler(FakeDatabase db)
        {
            _db = db;
        }

        public bool Handle(CreateAuthorCommand command)
        {
            if (string.IsNullOrWhiteSpace(command.Name))
            {
                throw new ArgumentException("The name of the Author can not be empty");
            }

            var newAuthor = new Author
            {
                Id = _db.Authors.Count + 1, // Generate a new ID
                Name = command.Name
            };

            _db.Authors.Add(newAuthor);
            return true;
        }
    }
}
