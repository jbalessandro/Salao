﻿using Salao.Domain.Abstract;
using Salao.Domain.Abstract.Cliente;
using Salao.Domain.Models.Cliente;
using Salao.Domain.Service.Cliente;
using Salao.Web.Areas.Empresa.Common;
using Salao.Web.Common;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Salao.Web.Areas.Empresa.Controllers
{
    [AreaAuthorize("Empresa", Roles = "salao_crud")]
    public class FilialController : Controller
    {
        IBaseService<Salao.Domain.Models.Cliente.Salao> _service;
        IBaseService<Salao.Domain.Models.Cliente.Empresa> _serviceEmpresa;
        ICadastroSalao _cadastro;

        public FilialController(IBaseService<Salao.Domain.Models.Cliente.Salao> service, IBaseService<Salao.Domain.Models.Cliente.Empresa> serviceEmpresa, ICadastroSalao cadastro)
        {
            _service = service;
            _serviceEmpresa = serviceEmpresa;
            _cadastro = cadastro;
        }

        // GET: Empresa/Filial
        public ActionResult Index()
        {
            var saloes = _service.Listar().Where(x => x.IdEmpresa == Identification.IdEmpresa);
            return View(saloes);
        }

        // GET: Empresa/Filial/Details/5
        public ActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var salao = _service.Find((int)id);

            if (salao.IdEmpresa != Identification.IdEmpresa)
            {
                return HttpNotFound();                
            }

            return View(salao);
        }

        // GET: Empresa/Filial/Create
        public ActionResult NovaFilial()
        {
            // empresa
            var empresa = Identification.Empresa;

            // promocao padrao
            var promocao = new PromocaoService().Get();

            var model = new CadastroSalao();
            model.Cortesia = true;
            model.Desconto = promocao.Desconto;
            model.DescontoCarencia = promocao.DescontoCarencia;
            model.TipoPessoa = empresa.TipoPessoa;
            model.IdEmpresa = empresa.Id;

            ViewBag.EmpresaFantasia = empresa.Fantasia;

            return View(model);
        }

        // POST: Empresa/Filial/Create
        [HttpPost]
        public ActionResult NovaFilial(CadastroSalao model)
        {
            try
            {
                model.AlteradoEm = DateTime.Now;
                model.Aprovado = false;
                model.Ativo = true;
                model.CadastradoEm = DateTime.Now;
                if (ModelState.IsValid)
                {
                    _cadastro.Gravar(model);
                    return RedirectToAction("Index");
                }

                throw new Exception();
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                ViewBag.EmpresaFantasia = Identification.EmpresaFantasia;
                return View(model);
            }
        }

        // GET: Empresa/Filial/Editar/5
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = _cadastro.Find((int)id);

            if (model == null)
            {
                return HttpNotFound();
            }

            if (model.IdEmpresa != Identification.IdEmpresa)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(model);
        }

        // POST: Empresa/Filial/Editar/5
        [HttpPost]
        public ActionResult Editar(CadastroSalao model)
        {
            try
            {
                model.AlteradoEm = DateTime.Now;
                if (ModelState.IsValid)
                {
                    _cadastro.Gravar(model);
                    return RedirectToAction("Index");
                }

                return View(model);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(model);
            }
        }

        // GET: Empresa/Filial/Delete/5
        public ActionResult Excluir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var salao = _service.Find((int)id);

            if (salao == null || salao.IdEmpresa != Identification.IdEmpresa)
	        {
                return HttpNotFound();
	        }

            return View(salao);
        }

        // POST: Empresa/Filial/Delete/5
        [HttpPost]
        public ActionResult Excluir(int id)
        {
            try
            {
                _service.Excluir(id);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                var salao = _service.Find(id);
                if (salao == null)
                {
                    return HttpNotFound();
                }
                return View(salao);
            }
        }
    }
}
