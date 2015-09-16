﻿using System.ComponentModel.DataAnnotations;

namespace Salao.Domain.Models.Admin
{
    public class UsuarioGrupo
    {
        [Key]
        public int IdUsuario { get; set; }

        [Key]
        public int IdGrupo { get; set; }
    }
}
