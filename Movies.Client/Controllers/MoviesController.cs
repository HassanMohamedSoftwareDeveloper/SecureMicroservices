// Ignore Spelling: Api

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.Client.ApiServices;
using Movies.Client.Models;

namespace Movies.Client.Controllers;

[Authorize]
public class MoviesController : Controller
{
    #region Fields :
    private readonly IMovieApiService _movieApiService;
    #endregion

    #region CTORS :
    public MoviesController(IMovieApiService movieApiService)
    {
        _movieApiService = movieApiService;
    }
    #endregion

    #region Actions :
    public async Task<IActionResult> Index()
    {
        return View(await _movieApiService.GetMoviesAsync());
    }

    public async Task<IActionResult> Details(int id)
    {
        var movie = await _movieApiService.GetMovieAsync(id);
        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Title,Genre,ReleaseDate,Owner,Rating,ImageUrl")] Movie movie)
    {
        if (ModelState.IsValid)
        {
            await _movieApiService.CreateMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }
        return View(movie);
    }

    public async Task<IActionResult> Edit(int id)
    {

        var movie = await _movieApiService.GetMovieAsync(id);
        if (movie == null)
        {
            return NotFound();
        }
        return View(movie);
    }

    // POST: Movies/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Genre,ReleaseDate,Owner,Rating,ImageUrl")] Movie movie)
    {
        if (id != movie.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await _movieApiService.UpdateMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }
        return View(movie);
    }

    // GET: Movies/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var movie = await _movieApiService.GetMovieAsync(id);
        if (movie == null)
        {
            return NotFound();
        }

        return View(movie);
    }

    // POST: Movies/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _movieApiService.DeleteMovieAsync(id);
        return RedirectToAction(nameof(Index));
    }
    #endregion

}
