﻿using Salao.Domain.Abstract;
using Salao.Domain.Models.Cliente;
using Salao.Domain.Service.Cliente;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Salao.Web.Areas.Cliente.Controllers
{
    [Authorize]
    public class ColaboradorController : Controller
    {
        IBaseService<Profissional> service;
        IBaseService<Salao.Domain.Models.Cliente.Salao> serviceSalao;

        public ColaboradorController()
        {
            service = new ProfissionalService();
            serviceSalao = new SalaoService();
        }

        // GET: Cliente/Colaborador
        public ActionResult Index(int idSalao)
        {
            var salao = serviceSalao.Find(idSalao);

            if (salao == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var profissionais = service.Listar()
                .Where(x => x.IdSalao == idSalao)
                .OrderBy(x => x.Nome);

            ViewBag.IdSalao = idSalao;
            ViewBag.IdEmpresa = salao.IdEmpresa;
            ViewBag.Fantasia = salao.Fantasia;
            return View(profissionais);
        }

        // GET: Cliente/Colaborador/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var colaborador = service.Find((int)id);

            if (colaborador == null)
            {
                return HttpNotFound();
            }

            return View(colaborador);
        }

        // GET: Cliente/Colaborador/Create
        public ActionResult Create(int idSalao)
        {
            var salao = serviceSalao.Find(idSalao);

            if (salao == null)
            {
                return HttpNotFound();
            }

            var profissional = new Profissional { IdSalao = idSalao };
            ViewBag.Fantasia = salao.Fantasia;

            return View(profissional);
        }

        // POST: Cliente/Colaborador/Create
        [HttpPost]
        public ActionResult Create([Bind(Include="IdSalao,Nome,Telefone,Email")] Profissional profissional)
        {
            try
            {
                profissional.AlteradoEm = DateTime.Now;
                TryUpdateModel(profissional);

                if (ModelState.IsValid)
                {
                    service.Gravar(profissional);
                    return RedirectToAction("Index", new { idSalao = profissional.IdSalao });
                }

                ViewBag.Fantasia = serviceSalao.Find(profissional.IdSalao).Fantasia;
                return View(profissional);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                ViewBag.Fantasia = serviceSalao.Find(profissional.IdSalao).Fantasia;
                return View(profissional);
            }
        }

        // GET: Cliente/Colaborador/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var profissional = service.Find((int)id);

            if (profissional == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            return View(profissional);
        }

        // POST: Cliente/Colaborador/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,IdSalao,Nome,Telefone,Email")] Profissional profissional)
        {
            try
            {
                profissional.AlteradoEm = DateTime.Now;
                TryUpdateModel(profissional);

                if (ModelState.IsValid)
                {
                    service.Gravar(profissional);
                    return RedirectToAction("Index", new { idSalao = profissional.IdSalao });
                }
                return View(profissional);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(profissional);
            }
        }

        // GET: Cliente/Colaborador/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var colaborador = service.Find((int)id);

            if (colaborador == null)
            {
                return HttpNotFound();
            }

            return View(colaborador);
        }

        // POST: Cliente/Colaborador/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, int idSalao)
        {
            try
            {
                service.Excluir(id);
                return RedirectToAction("Index", new { idSalao = idSalao });
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                var colaborador = service.Find(id);
                if (colaborador == null)
                {
                    return HttpNotFound();
                }
                return View(colaborador);
            }
        }
    }
}