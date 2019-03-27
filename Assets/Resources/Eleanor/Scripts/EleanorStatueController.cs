using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.UIElements.GraphView;
using UnityEngine;

public class EleanorStatueController : Tile
{
    // Start is called before the first frame update
    private bool _pray;
    private bool _trade;
    private bool _tradeReady;
    public SpriteRenderer PrayLight;
    public PrayScript PrayLightScript;
    public SpriteRenderer TradeLight;
    public AudioSource Audio;
    public AudioClip Aah;
    private bool _audioPlayed;

    private Tile tileholdingus;

    public GameObject TradeMark;
    private UtiCursedController _cursedController;

    private float _timer = 2;

    public void Update()
    {

        if (_cursedController == null)
        {
            _cursedController = GameObject.Find("UtiCursed(Clone)").GetComponent<UtiCursedController>();
        }

        if (_tradeReady && Input.GetKeyDown(KeyCode.T))
        {
            _trade = true;
            Destroy(tileholdingus.tileWereHolding.gameObject);
            if (_cursedController != null && _cursedController.cursed > 0)
            {
                _cursedController.cursed--;
            }


            tileholdingus.health+=2;
        }
        if (_trade)
        {
            if (!_audioPlayed)
            {
                Audio.PlayOneShot(Aah);
                _audioPlayed = true;
            }
            else
            {
                _timer -= Time.deltaTime;
            }

            if (_timer > 0){
            TradeLight.color = new Color(1,1,1, Mathf.Lerp(TradeLight.color.a, 1, 0.1f));  
            }
            else 
            {
                TradeLight.color = new Color(1,1,1, Mathf.Lerp(TradeLight.color.a, 0, 0.1f)); 
            }
            
            if (_timer <0 && TradeLight.color.a < 0.05f) Destroy(gameObject);
        }
        if (_pray)
        {          
            if (!_audioPlayed)
            {
                Audio.PlayOneShot(Aah);
                _audioPlayed = true;
            }
            else
            {
                _timer -= Time.deltaTime;
            }

            if (_timer > 0)
            {
                PrayLight.color = new Color(1, 1, 1, Mathf.Lerp(PrayLight.color.a, 1, 0.1f));
            }
            else 
            {
                PrayLight.color = new Color(1,1,1, Mathf.Lerp(PrayLight.color.a, 0, 0.1f)); 
            }

            if (_timer <0 && PrayLight.color.a < 0.05f)
            {
                foreach (Tile enemy in PrayLightScript.Enemies)
                {
                    Debug.Log(enemy.tileName);
                    enemy.takeDamage(this,10);
                }
                Destroy(gameObject);
            } 
        }

    }
    
    public override void pickUp(Tile tilePickingUsUp)
    {
        if (!tilePickingUsUp.hasTag(TileTags.Player))
            return;
            _pray = true;
        Debug.Log(_cursedController.cursed);
        if (_cursedController != null && _cursedController.cursed > 0)
        {
            _cursedController.cursed--;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Tile maybetile = other.GetComponent<Tile>();
        if (maybetile == null || !maybetile.hasTag(TileTags.Player)) return;      
        if (maybetile.tileWereHolding != null && maybetile.tileWereHolding.tileName.Contains("Apple"))
        {
            TradeMark.SetActive(true);
            _tradeReady = true;
            tileholdingus = maybetile;


        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Tile maybetile = other.GetComponent<Tile>();
        if (maybetile == null || !maybetile.hasTag(TileTags.Player)) return;      
        if (maybetile.tileWereHolding != null && maybetile.tileWereHolding.tileName.Contains("Apple"))
        {
            TradeMark.SetActive(false);
            _tradeReady = false;
        }
    }
}
