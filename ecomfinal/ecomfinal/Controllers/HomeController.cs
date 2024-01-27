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
            // Exemple : Obtenez les produits à partir de la base de données ou d'un service
            List<int> produits = GetProduitsFromDatabase();

            // Utilisez ViewBag pour transmettre les données à la vue
            ViewBag.Produits = produits;

            return View();
        }

        public IActionResult AddToCart(int productId)
        {
            // Récupérer le panier de l'utilisateur (exemple simplifié)
            var cart = _context.Panier
                .Include(c => c.produit) // Assurez-vous que votre modèle Panier a une propriété 'Produits'
                .FirstOrDefault();


            if (cart == null)
            {
                // Si le panier n'existe pas, vous pouvez choisir de créer un nouveau panier ici
                cart = new Panier();
                _context.Panier.Add(cart);
                _context.SaveChanges(); // Sauvegardez les changements pour obtenir l'ID du nouveau panier
            }

            // Assurez-vous que la propriété 'produit' du panier est initialisée
            if (cart.produit == null)
            {
                cart.produit = new List<Produit>();
            }


            // Vérifier si le produit est déjà dans le panier
            var existingItem = cart?.produit?.FirstOrDefault(item => item.id == productId);

            if (existingItem != null)
            {
                // Si le produit est déjà dans le panier, augmenter la quantité
                existingItem.Quantite++;
            }
            else
            {
                // Si le produit n'est pas dans le panier, ajouter un nouvel élément
                var productToAdd = _context.Produit.FirstOrDefault(p => p.id == productId);

                if (productToAdd != null)
                {
                    var newItem = new Produit
                    {
                        id = productToAdd.id, // Correction du nom de la propriété
                        Nom = productToAdd.Nom,
                        Prix = productToAdd.Prix,
                        Quantite = 1
                    };

                    cart.produit.Add(newItem);
                }
            }

            // Enregistrer les modifications dans la base de données
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
