using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Command;
using Application.DTOs;
using Application.Query;
using CandidateMVC.Helpers;
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

        public async Task<IActionResult> Index()
        {
            var model = await candidateQuery.GetAllCandidatesAsync(configuration.GetValue<string>("BaseURLApi"));
            return View(model);
        }

        public async Task<IActionResult> Details(int? id)
        {
            var candidate = await candidateQuery.GetCandidateByIdAsync(configuration.GetValue<string>("BaseURLApi"), id.GetValueOrDefault(0));

            if (candidate == null)
                return NotFound();

            return View(candidate);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CandidateId,Name,LastName,DateOfBirth,Email,PhoneNumber,ResumeFile,Jobs")] CandidateDTO candidate)
        {
            if (ModelState.IsValid)
            {
                if (!FileUploadHelper.CheckFileExtension(candidate.ResumeFile, configuration.GetValue<string>("FileExtensions")))
                    throw new Exception("File extension not valid. Please select .pdf or .doc or .docx files.");

                candidate.Resume = await FileUploadHelper.CopyAndCreateFileAsync(candidate.ResumeFile);

                await candidateCommand.CreateCommandAsync(configuration.GetValue<string>("BaseURLApi"), candidate);
                return RedirectToAction(nameof(Index));
            }

            return View(candidate);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var candidate = await candidateQuery.GetCandidateByIdAsync(configuration.GetValue<string>("BaseURLApi"), id.GetValueOrDefault(0));

            if (candidate == null)
                return NotFound();

            return View(candidate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("CandidateId,Name,LastName,DateOfBirth,Email,PhoneNumber,ResumeFile,Jobs")] CandidateDTO candidate)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await candidateCommand.EditCommandAsync(configuration.GetValue<string>("BaseURLApi"), candidate);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(candidate);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var candidate = await candidateQuery.GetCandidateByIdAsync(configuration.GetValue<string>("BaseURLApi"), id.GetValueOrDefault(0));

            if (candidate == null)
                return NotFound();

            return View(candidate);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await candidateCommand.DeleteCommandAsync(configuration.GetValue<string>("BaseURLApi"), id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult GetPartialJob(int countJobs)
        {
            return PartialView("_CandidateJob", new JobDTO { JobId = countJobs, CompanyName = "", Period = DateTime.Now });
        }
    }
}
