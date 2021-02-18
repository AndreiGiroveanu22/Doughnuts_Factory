﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DoughnutsFactory.Data;
using DoughnutsFactory.Models;

namespace DoughnutsFactory.Pages.Doughnuts
{
    public class EditModel : PageModel
    {
        private readonly DoughnutsFactory.Data.DoughnutsFactoryContext _context;

        public EditModel(DoughnutsFactory.Data.DoughnutsFactoryContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Doughnut Doughnuts { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Doughnuts = await _context.Doughnuts.FirstOrDefaultAsync(m => m.ID == id);

            if (Doughnuts == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Doughnuts).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoughnutsExists(Doughnuts.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool DoughnutsExists(int id)
        {
            return _context.Doughnuts.Any(e => e.ID == id);
        }
    }
}
