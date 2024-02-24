namespace RelevésMétéo
{
  internal class Program
  {

    static string[] lignes = File.ReadAllLines("meteoParis.csv");

    static void AfficherListe()
    {
      float sommeTemp = 0f;
      for (int i = 1; i < lignes.Length; i++)
      {
        // Simplifie le format des heures d'ensoleillement
        string ligne = lignes[i].Replace("h ", "h").Replace("min", "");

        // récupère les infos de la ligne dans un tableau
        string[] infos = ligne.Split(';');

        // Construit une ligne sous la forme souhaitéé
        Console.WriteLine($"""
         {infos[0]}/{infos[1]} : [{infos[2]} ; {infos[3]}]°C{"\t"}{infos[6]} de soleil{"\t"}{infos[7]} mm de pluie
         """);

        // Ajoute la température moyenne au cumul
        if (float.TryParse(infos[4], out float temp)) sommeTemp += temp;
      }
      Console.WriteLine($"\nT° moyenne globale : {sommeTemp / (lignes.Length - 1)}");
    }

    static void AfficherTableau()
    {
      float sommeTemperature = 0f;

      for (int i = 0; i < lignes.Length; i++)
      {

        string[] infoLigne = lignes[i].Split(';');
        string entete;
        string ligne;

        if (i == 0)
        {
          entete = $"""
           + -------- + -------- + -------- + ---------------- + ------------ +
           |   Mois   |  T° min  |  T° max  |  Soleil (durée)  |  Pluie (mm)  |
           + -------- + -------- + -------- + ---------------- + ------------ +
           """;
          Console.WriteLine(entete);
        }
        else if (i != 0)
        {
          ligne = $"""
           | {infoLigne[0],3}/{infoLigne[1]} | {infoLigne[2],8:N1} | {infoLigne[3],8:N1} | {infoLigne[6],16} | {infoLigne[7],12:N1} |
           """;
          Console.WriteLine(ligne);

          if (i == lignes.Length - 1)
          {
            Console.WriteLine("+ -------- + -------- + -------- + ---------------- + ------------ +");
          }
        }

        if (float.TryParse(infoLigne[4], out float temp)) sommeTemperature += temp;

      }

      Console.WriteLine($"\nLa température moyenne est de : {sommeTemperature / (lignes.Length - 1)}°");

    }

    static void AfficherTableauSolutionByCyril()
    {
      const string entetes = """
			Mois | T° min | T° max | Soleil | Pluie (mm)
		-----------------------------------------------
		""";
      Console.WriteLine(entetes);

      string[] lignes = File.ReadAllLines("meteoParis.csv");
      for (int i = 1; i < lignes.Length; i++)
      {
        // Simplifie le format des heures d'ensoleillement
        string ligne = lignes[i].Replace("h ", "h").Replace("min", "");

        // Récupère les infos de la ligne dans un tableau
        string[] infos = ligne.Split(';');

        // Transforme les chaînes en nombres
        if (float.TryParse(infos[2], out float tmin) &&
          float.TryParse(infos[3], out float tmax) &&
          float.TryParse(infos[7], out float pluie))
        {
          // Affiche la ligne sous la forme souhaitée
          Console.WriteLine($"{infos[0]}/{infos[1]} | {tmin,6:N1} | {tmax,6:N1} | {infos[6],6} | {pluie,10:N1}");
        }
        else
          Console.WriteLine("Erreur à la ligne {0}", i);
      }
    }

    static void Main(string[] args)
    {
      // Afficher la liste météorologique en liste
      AfficherListe();

      // Afficher la liste météorologique en tableau
      AfficherTableau();

      // Afficher la solution proposé par Cyril
      AfficherTableauSolutionByCyril();
    }
  }
}