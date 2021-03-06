﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Salao.Domain.Models.Admin
{
    public class SubArea
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe a sub área do serviço")]
        [StringLength(20, ErrorMessage = "A sub área do serviço é formada por no máximo 20 caracteres")]
        [Display(Name = "Sub área do serviço",Prompt="Pedicure, manicure, pintura...")]
        public string Descricao { get; set; }

        public bool Ativo { get; set; }

        [HiddenInput(DisplayValue=false)]
        [Range(1,int.MaxValue, ErrorMessage="Selecione a área do serviço")]
        [Display(Name="Área")]
        public int IdArea { get; set; }

        [Required]
        [Display(Name = "Alterado por")]
        public int AlteradoPor { get; set; }

        [Required]
        [Display(Name = "Alterado em")]
        public DateTime AlteradoEm { get; set; }

        [NotMapped]
        [Display(Name = "Usuário")]
        public virtual Usuario Usuario
        {
            get
            {
                return new Salao.Domain.Service.Admin.UsuarioService().Find(AlteradoPor);
            }
            set { }
        }

        [NotMapped]
        [Display(Name = "Área")]
        public virtual Area Area
        {
            get
            {
                return new Salao.Domain.Service.Admin.AreaService().Find(IdArea);
            }
        }
    }
}
