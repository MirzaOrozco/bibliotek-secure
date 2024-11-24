using Application.Commands.Authors;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handler
{
    public class UpdateAuthorCommandHandler
    {
        private readonly FakeDatabase _db;

        public UpdateAuthorCommandHandler(FakeDatabase db)
        {
            _db = db;
        }

        public bool Handle(UpdateAuthorCommand command)
        {
            if (string.IsNullOrWhiteSpace(command.NewName))
            {
                throw new ArgumentException("The new Author's ¨name can not be empty.");
            }

            var author = _db.Authors.Find(a => a.Id == command.Id);
            if (author == null)
            {
                throw new KeyNotFoundException("The Author doesn't exist.");
            }

            author.Name = command.NewName;
            return true;
        }
    }
}
