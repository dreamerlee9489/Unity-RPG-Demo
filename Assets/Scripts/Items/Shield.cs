using App.Manager;
using App.Config;
using App.UI;
using UnityEngine;
using App.Control;

namespace App.Items
{
    public class Shield : Equipment
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