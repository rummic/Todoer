using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Todoer.Data;
using Todoer.Enums;
using Todoer.Models.DbModels;
using Todoer.Models.DtoModels;
using Todoer.Services.Interfaces;
using Task = Todoer.Models.DbModels.Task;

namespace Todoer.Controllers
{
    public class TasksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IPriorityConverterService _priorityConverterService;
        public TasksController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IPriorityConverterService priorityConverterService)
        {
            _context = context;
            _userManager = userManager;
            _priorityConverterService = priorityConverterService;
        }

        // GET: Tasks
        public async Task<IActionResult> Index()
        {
            var dbTasks = await _context.Tasks.ToListAsync();
            var userDbTasks = dbTasks.Where(x => x.ApplicationUserId == _userManager.GetUserId(User) && x.Done == false).OrderByDescending(x => x.Priority).ThenByDescending(x => x.Deadline);
            List<IndexTaskDto> result = new List<IndexTaskDto>();
            foreach (var userDbTask in userDbTasks)
            {
                var he = _priorityConverterService.EnumToString(userDbTask.Priority);
                    result.Add(new IndexTaskDto
                {
                    DeadlineDate = userDbTask.Deadline.ToString("dd-MM-yyyy"),
                    DeadlineTime = userDbTask.Deadline.ToString("HH:mm"),
                    Description = userDbTask.Description,
                    Priority = _priorityConverterService.EnumToString(userDbTask.Priority),
                    Title = userDbTask.Title,
                    Id = userDbTask.Id
                });
            }
            return View(result);
        }

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // GET: Tasks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,DeadlineDate,DeadlineTime,Priority")] CreateTaskDto givenTask)
        {
            if (ModelState.IsValid)
            {
                var task = new Task
                {
                    CreatedAt = DateTime.Now,
                    ApplicationUserId = _userManager.GetUserId(User),
                    Deadline = givenTask.DeadlineDate.Date + givenTask.DeadlineTime,
                    Priority = givenTask.Priority,
                    Description = givenTask.Description,
                    Title = givenTask.Title,
                };
                _context.Add(task);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            var taskDto = new CreateTaskDto
            {
                Priority = task.Priority,
                DeadlineDate = task.Deadline.Date,
                DeadlineTime = task.Deadline.TimeOfDay,
                Description = task.Description,
                Title = task.Title
            };
            return View(taskDto);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,Description,DeadlineDate,DeadlineTime,Priority")] CreateTaskDto task)
        {
            var taskFromDb = _context.Tasks.FirstOrDefault(x => x.Id == id);
            if (taskFromDb == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    taskFromDb.Priority = task.Priority;
                    taskFromDb.Deadline = task.DeadlineDate + task.DeadlineTime;
                    taskFromDb.Description = task.Description;
                    taskFromDb.Title = task.Title;
                    _context.Update(taskFromDb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            task.Done = true;
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
