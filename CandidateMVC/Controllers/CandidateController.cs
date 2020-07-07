using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Command;
using Application.DTOs;
using Application.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace CandidateService.Controllers
{
    public class CandidateController : Controller
    {
        private readonly ICandidateQuery candidateQuery;
        private readonly ICandidateCommand candidateCommand;
        private readonly IConfiguration configuration;

        public CandidateController(ICandidateQuery candidateQuery,
                                   IConfiguration configuration,
                                   ICandidateCommand candidateCommand)
        {
            this.candidateQuery = candidateQuery;
            this.configuration = configuration;
            this.candidateCommand = candidateCommand;
        }

        // GET: Candidates
        public async Task<IActionResult> Index()
        {
            var model = await candidateQuery.GetAllCandidatesAsync(configuration.GetValue<string>("BaseURLApi"));
            return View(model);
        }

        //GET: Candidates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var candidate = await candidateQuery.GetCandidateByIdAsync(configuration.GetValue<string>("BaseURLApi"), id.GetValueOrDefault(0));

            if (candidate == null)
                return NotFound();

            return View(candidate);
        }

        // GET: Candidates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Candidates/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CandidateId,Name,LastName,DateOfBirth,Email,PhoneNumber,Resume")] CandidateDTO candidate)
        {
            if (ModelState.IsValid)
            {
                await candidateCommand.ExecuteCommandAsync(configuration.GetValue<string>("BaseURLApi"), candidate, Method.POST);
                return RedirectToAction(nameof(Index));
            }
            return View(candidate);
        }

        // GET: Candidates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var candidate = await candidateQuery.GetCandidateByIdAsync(configuration.GetValue<string>("BaseURLApi"), id.GetValueOrDefault(0));

            if (candidate == null)
                return NotFound();

            return View(candidate);
        }

        // POST: Candidates/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("CandidateId,Name,LastName,DateOfBirth,Email,PhoneNumber,Resume")] CandidateDTO candidate)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await candidateCommand.ExecuteCommandAsync(configuration.GetValue<string>("BaseURLApi"), candidate, Method.PUT);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(candidate);
        }

        // GET: Candidates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var candidate = await candidateQuery.GetCandidateByIdAsync(configuration.GetValue<string>("BaseURLApi"), id.GetValueOrDefault(0));

            if (candidate == null)
                return NotFound();

            return View(candidate);
        }

        // DELETE: Candidates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var candidate = await candidateQuery.GetCandidateByIdAsync(configuration.GetValue<string>("BaseURLApi"), id);

            await candidateCommand.ExecuteCommandAsync(configuration.GetValue<string>("BaseURLApi"), candidate, Method.DELETE);

            return RedirectToAction(nameof(Index));
        }
    }
}
