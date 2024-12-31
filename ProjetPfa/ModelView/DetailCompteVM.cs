using System.ComponentModel.DataAnnotations;



namespace ProjetPfa.ModelView
{
    public class DetailCompteVM
    {
        [Required(ErrorMessage = "Le nom d'affichage est requis.")]
        [Display(Name = "Nom d'affichage")]
        public string DisplayName { get; set; }

        [Required(ErrorMessage = "La description est requise.")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Photo de profil")]
        public byte[] ProfilePicture { get; set; }

        [Required(ErrorMessage = "Au moins une langue est requise.")]
        [Display(Name = "Langues")]
        public List<string> Languages { get; set; }

        [Required(ErrorMessage = "Au moins un niveau de langue est requis.")]
        [Display(Name = "Niveaux de langue")]
        public List<string> LanguageLevels { get; set; }

        [Display(Name = "Nom ou le numéro de votre organisation")]
        public string OrgName { get; set; } // Added property
    }
}


