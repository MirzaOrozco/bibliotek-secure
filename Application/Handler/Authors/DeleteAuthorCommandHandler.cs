using API.Commands.Authors;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Handler.Authors
{
    public class DeleteAuthorCommandHandler
    {
        private readonly FakeDatabase _db;

        public DeleteAuthorCommandHandler(FakeDatabase db)
        {
            _db = db;
        }

        public bool Handle(DeleteAuthorCommand command)
        {
            var author = _db.Authors.Find(a => a.Id == command.Id);
            if (author == null)
            {
                throw new KeyNotFoundException("The Author doesn't exist.");
            }

            _db.Authors.Remove(author);
            return true;
        }
    }
}

