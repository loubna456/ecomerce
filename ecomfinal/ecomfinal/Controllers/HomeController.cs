using ecomfinal.Data;
using ecomfinal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq; // Ajout de la directive using pour System.Linq
using System.Collections.Generic; // Ajout de la directive using pour System.Collections.Generic



namespace ecomfinal.Controllers
{
    public class HomeController : Controller
    {
       
        private readonly ecomfinalContext _context;

        public HomeController( ecomfinalContext context)
        {
        
            _context = context;
        }

        public IActionResult Index()
        {
            // Exemple : Obtenez les produits � partir de la base de donn�es ou d'un service
            List<int> produits = GetProduitsFromDatabase();

            // Utilisez ViewBag pour transmettre les donn�es � la vue
            ViewBag.Produits = produits;

            return View();
        }

        public IActionResult AddToCart(int productId)
        {
            // R�cup�rer le panier de l'utilisateur (exemple simplifi�)
            var cart = _context.Panier
                .Include(c => c.produit) // Assurez-vous que votre mod�le Panier a une propri�t� 'Produits'
                .FirstOrDefault();


            if (cart == null)
            {
                // Si le panier n'existe pas, vous pouvez choisir de cr�er un nouveau panier ici
                cart = new Panier();
                _context.Panier.Add(cart);
                _context.SaveChanges(); // Sauvegardez les changements pour obtenir l'ID du nouveau panier
            }

            // Assurez-vous que la propri�t� 'produit' du panier est initialis�e
            if (cart.produit == null)
            {
                cart.produit = new List<Produit>();
            }


            // V�rifier si le produit est d�j� dans le panier
            var existingItem = cart?.produit?.FirstOrDefault(item => item.id == productId);

            if (existingItem != null)
            {
                // Si le produit est d�j� dans le panier, augmenter la quantit�
                existingItem.Quantite++;
            }
            else
            {
                // Si le produit n'est pas dans le panier, ajouter un nouvel �l�ment
                var productToAdd = _context.Produit.FirstOrDefault(p => p.id == productId);

                if (productToAdd != null)
                {
                    var newItem = new Produit
                    {
                        id = productToAdd.id, // Correction du nom de la propri�t�
                        Nom = productToAdd.Nom,
                        Prix = productToAdd.Prix,
                        Quantite = 1
                    };

                    cart.produit.Add(newItem);
                }
            }

            // Enregistrer les modifications dans la base de donn�es
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        private List<int> GetProduitsFromDatabase()
        {
            return _context.Produit.Select(p => p.id).ToList();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
