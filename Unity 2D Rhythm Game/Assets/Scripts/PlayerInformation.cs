using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;

public static class PlayerInformation
{

    public static int maxCombo { get; set; }
    public static float score { get; set; }
    public static string selectedMusic { get; set; }
    public static string musicTitle { get; set; }
    public static string musicArtist { get; set; }
    public static Firebase.Auth.FirebaseAuth auth;

    public static DatabaseReference GetDatabaseReference()
    {
        DatabaseReference reference;
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        return reference;
    }

}
