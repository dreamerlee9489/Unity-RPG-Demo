using UnityEngine;
using App.Config;
using App.Manager;
using App.UI;
using App.Control;

namespace App.Items
{
    public class Helmet : Equipment
    {
        public override void Use(CombatEntity user)
        {
            throw new System.NotImplementedException();
        }

        protected override void OnTriggerEnter(Collider other)
        {
            throw new System.NotImplementedException();
        }
    }
}