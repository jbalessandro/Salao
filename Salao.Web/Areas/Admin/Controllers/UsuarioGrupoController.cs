﻿using Salao.Domain.Abstract;
using Salao.Domain.Abstract.Admin;
using Salao.Domain.Models.Admin;
using Salao.Domain.Service.Admin;
using Salao.Web.Areas.Admin.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Salao.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class UsuarioGrupoController : Controller
    {
        IBaseService<Usuario> serviceUsuario;
        IBaseService<Grupo> serviceGrupo;
        IUsuarioGrupo serviceUsuarioGrupo;

        public UsuarioGrupoController()
        {
            serviceUsuario = new UsuarioService();
            serviceGrupo = new GrupoService();
            serviceUsuarioGrupo = new UsuarioGrupoService();
        }

        //
        // GET: /Admin/UsuarioGrupo/
        public ActionResult Index(int id)
        {
            // usuario selecionado
            var usuario = serviceUsuario.Find(id);
            
            // grupos disponiveis
            var grupos = serviceGrupo.Listar().Where(x => x.Ativo == true).OrderBy(x => x.Descricao).ToList();

            // lista retorno
            var gruposUsuario = new List<GruposUsuario>();
            foreach (var item in grupos)
            {
                gruposUsuario.Add(new GruposUsuario
                {
                    Descricao = item.Descricao,
                    Id = item.Id,
                    Selecionado = (serviceUsuarioGrupo.Listar().Where(x => x.IdGrupo == item.Id && x.IdUsuario == id).Count() > 0)
                });
            }

            ViewBag.IdUsuario = id;
            ViewBag.NomeUsuario = usuario.Nome;

            return View(gruposUsuario);
        }

        [HttpPost]
        public ActionResult Index(int[] selecionado, int idUsuario)
        {
            // grava grupos do usuario
            serviceUsuarioGrupo.Gravar(idUsuario, selecionado);

            return RedirectToAction("Index", "Usuario");
        }
	}
}