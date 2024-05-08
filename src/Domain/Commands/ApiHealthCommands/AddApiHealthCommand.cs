using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Models;
using OeuilDeSauron.Models;

namespace OeuilDeSauron.Domain.Commands.ApiHealthCommands
{
    public class AddApiHealthCommand : IRequest<ApiHealth>
    {
        public ApiHealth ApiHealth { get; set; }
        public AddApiHealthCommand(ApiHealth apiHealth)
        {

            ApiHealth = apiHealth;

        }
    }
}
