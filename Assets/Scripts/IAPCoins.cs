using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using System;

public class IAPCoins : MonoBehaviour, IStoreListener
{
    private static IStoreController m_StoreController;          // The Unity Purchasing system.
    private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.
    public GameObject noAdsButton, unlockPurchaseAllButtonMainMenu, unlockPurchaseAllButtonShop, /*noAdsPanel,*/ purchasePanel;
    public GameObject purchaseAllMouseDragonsGo, purchaseAllTigerDragonsGo, purchaseAllSeaDragonsGo;
    // Product identifiers for all products capable of being purchased: 
    // "convenience" general identifiers for use with Purchasing, and their store-specific identifier 
    // counterparts for use with and outside of Unity Purchasing. Define store-specific identifiers 
    // also on each platform's publisher dashboard (iTunes Connect, Google Play Developer Console, etc.)

    // General product identifiers for the consumable, non-consumable, and subscription products.
    // Use these handles in the code to reference which product to purchase. Also use these values 
    // when defining the Product Identifiers on the store. Except, for illustration purposes, the 
    // kProductIDSubscription - it has custom Apple and Google identifiers. We declare their store-
    // specific mapping to Unity Purchasing's AddProduct, below.
    //public static string kProductIDConsumable = "consumable";
    //public static string kProductIDNonConsumable = "nonconsumable";
    //public static string kProductIDSubscription = "subscription";
    [SerializeField]public static string firstPack = "1000coins";
    public static string secondPack = "2400coins";
    public static string thirdPack = "4000coins";
    public static string fourthPack = "9000coins";
    public static string noAds = "noadsdg";
    public static string purchaseAllMouseDragons = "purchaseallmousedragons";
    public static string purchaseAllTigerDragons = "purchasealltigerdragons";
    public static string purchaseAllSeaDragons = "purchaseallseadragons";
    public static string purchaseAll = "purchaseall";
    // Apple App Store-specific product identifier for the subscription product.
    private static string kProductNameAppleSubscription = "com.unity3d.subscription.new";

    // Google Play Store-specific product identifier subscription product.
    private static string kProductNameGooglePlaySubscription = "com.unity3d.subscription.original";

    void Start()
    {
        // If we haven't set up the Unity Purchasing reference
        if (m_StoreController == null)
        {
            // Begin to configure our connection to Purchasing
            InitializePurchasing();
        }
        if (PlayerPrefs.GetInt("SHOW_ADS") == 1)
        {
            noAdsButton.gameObject.SetActive(false);
        }
        CheckingPurchases();
    }

    [Obsolete]
    public void InitializePurchasing()
    {
        // If we have already connected to Purchasing ...
        if (IsInitialized())
        {
            // ... we are done here.
            return;
        }

        // Create a builder, first passing in a suite of Unity provided stores.
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        // Add a product to sell / restore by way of its identifier, associating the general identifier
        // with its store-specific identifiers.
        builder.AddProduct(firstPack, ProductType.Consumable);
        builder.AddProduct(secondPack, ProductType.Consumable);
        builder.AddProduct(thirdPack, ProductType.Consumable);
        builder.AddProduct(fourthPack, ProductType.Consumable);
        builder.AddProduct(noAds, ProductType.Consumable);
        builder.AddProduct(purchaseAllMouseDragons, ProductType.Consumable);
        builder.AddProduct(purchaseAllTigerDragons, ProductType.Consumable);
        builder.AddProduct(purchaseAllSeaDragons, ProductType.Consumable);
        builder.AddProduct(purchaseAll, ProductType.Consumable);
        // Continue adding the non-consumable product.
        // builder.AddProduct(kProductIDNonConsumable, ProductType.NonConsumable);
        // And finish adding the subscription product. Notice this uses store-specific IDs, illustrating
        // if the Product ID was configured differently between Apple and Google stores. Also note that
        // one uses the general kProductIDSubscription handle inside the game - the store-specific IDs 
        // must only be referenced here. 
        //builder.AddProduct(kProductIDSubscription, ProductType.Subscription, new IDs(){
        //  { kProductNameAppleSubscription, AppleAppStore.Name },
        //   { kProductNameGooglePlaySubscription, GooglePlay.Name },
        // });

        // Kick off the remainder of the set-up with an asynchrounous call, passing the configuration 
        // and this class' instance. Expect a response either in OnInitialized or OnInitializeFailed.
        UnityPurchasing.Initialize(this, builder);
    }


    private bool IsInitialized()
    {
        // Only say we are initialized if both the Purchasing references are set.
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }


    public void BuyFirstPack()
    {
        BuyProductID(firstPack);
    }
    public void BuySecondPack()
    {
        BuyProductID(secondPack);
    }
    public void BuyThirdPack()
    {
        BuyProductID(thirdPack);
    }
    public void BuyFourthPack()
    {
        BuyProductID(fourthPack);
    }
    public void BuyNoAdsPack()
    {
        BuyProductID(noAds);
    }
    public void BuyPurchaseAllPackMouseDragons()
    {
        BuyProductID(purchaseAllMouseDragons);
    }public void BuyPurchaseAllTigerDragons()
    {
        BuyProductID(purchaseAllTigerDragons);
    }public void BuyPurchaseAllPackSeaDragons()
    {
        BuyProductID(purchaseAllSeaDragons);
    }public void BuyPurchaseAllPack()
    {
        BuyProductID(purchaseAll);
    }
    //public void BuyNonConsumable()
    //{
    //    // Buy the non-consumable product using its general identifier. Expect a response either 
    //    // through ProcessPurchase or OnPurchaseFailed asynchronously.
    //    //BuyProductID(kProductIDNonConsumable);
    //}


    //public void BuySubscription()
    //{
    //    // Buy the subscription product using its the general identifier. Expect a response either 
    //    // through ProcessPurchase or OnPurchaseFailed asynchronously.
    //    // Notice how we use the general product identifier in spite of this ID being mapped to
    //    // custom store-specific identifiers above.
    //    BuyProductID(kProductIDSubscription);
    //}


    void BuyProductID(string productId)
    {
        // If Purchasing has been initialized ...
        if (IsInitialized())
        {
            // ... look up the Product reference with the general product identifier and the Purchasing 
            // system's products collection.
            Product product = m_StoreController.products.WithID(productId);

            // If the look up found a product for this device's store and that product is ready to be sold ... 
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
                // asynchronously.
                m_StoreController.InitiatePurchase(product);
            }
            // Otherwise ...
            else
            {
                // ... report the product look-up failure situation  
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        // Otherwise ...
        else
        {
            // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
            // retrying initiailization.
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }


    // Restore purchases previously made by this customer. Some platforms automatically restore purchases, like Google. 
    // Apple currently requires explicit purchase restoration for IAP, conditionally displaying a password prompt.
    public void RestorePurchases()
    {
        // If Purchasing has not yet been set up ...
        if (!IsInitialized())
        {
            // ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }

        // If we are running on an Apple device ... 
        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            // ... begin restoring purchases
            Debug.Log("RestorePurchases started ...");

            // Fetch the Apple store-specific subsystem.
            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
            // Begin the asynchronous process of restoring purchases. Expect a confirmation response in 
            // the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
            apple.RestoreTransactions((result) => {
                // The first phase of restoration. If no more responses are received on ProcessPurchase then 
                // no purchases are available to be restored.
                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
            });
        }
        // Otherwise ...
        else
        {
            // We are not running on an Apple device. No work is necessary to restore purchases.
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }


    //  
    // --- IStoreListener
    //

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        // Purchasing has succeeded initializing. Collect our Purchasing references.
        Debug.Log("OnInitialized: PASS");

        // Overall Purchasing system, configured with products for this application.
        m_StoreController = controller;
        // Store specific subsystem, for accessing device-specific store features.
        m_StoreExtensionProvider = extensions;
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }


    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (String.Equals(args.purchasedProduct.definition.id, firstPack, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // TODO: The subscription item has been successfully purchased, grant this to the player.
            //int coins = PlayerPrefs.GetInt("Coins", 0);
            //PlayerPrefs.SetInt("Coins",  coins + 100);
            //ScoreManager.instance.IAPAddDiamondCoinBalance(1000);
            //UIController.instance.coinAnimationDiamond.AddCoins(GameManager.instance.pack1Diamond.transform.position, 5);
            //GameManager.Instance.starsCollected = PlayerPrefs.GetInt("Coins", 0);
            //PlayerPrefs.SetInt("numberOfDeaths", 4);
            GameManager.instance.currencyManager.AddCurrency(1000);


        }
        // Or ... an unknown product has been purchased by this user. Fill in additional products here....
        else
        {
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
        }
        if (String.Equals(args.purchasedProduct.definition.id, secondPack, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // TODO: The subscription item has been successfully purchased, grant this to the player.
            //int coins = PlayerPrefs.GetInt("Coins", 0);
            //PlayerPrefs.SetInt("Coins", coins + 250);
            //ScoreManager.instance.IAPAddDiamondCoinBalance(2400);
            //UIController.instance.coinAnimationDiamond.AddCoins(GameManager.instance.pack2Diamond.transform.position, 5);
            //GameManager.Instance.starsCollected = PlayerPrefs.GetInt("Coins", 0);
            //PlayerPrefs.SetInt("numberOfDeaths", 4);
            GameManager.instance.currencyManager.AddCurrency(2400);
        }
        // Or ... an unknown product has been purchased by this user. Fill in additional products here....
        else
        {
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
        }
        if (String.Equals(args.purchasedProduct.definition.id, thirdPack, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // TODO: The subscription item has been successfully purchased, grant this to the player.
            //int coins = PlayerPrefs.GetInt("Coins", 0);
            //PlayerPrefs.SetInt("Coins", coins + 500);
            //ScoreManager.instance.IAPAddDiamondCoinBalance(4000);
            //UIController.instance.coinAnimationDiamond.AddCoins(GameManager.instance.pack3Diamond.transform.position, 5);
            //GameManager.Instance.starsCollected = PlayerPrefs.GetInt("Coins", 0);
            //PlayerPrefs.SetInt("numberOfDeaths", 4);
            GameManager.instance.currencyManager.AddCurrency(4000);
        }
        // Or ... an unknown product has been purchased by this user. Fill in additional products here....
        else
        {
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
        }
        if (String.Equals(args.purchasedProduct.definition.id, fourthPack, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // TODO: The subscription item has been successfully purchased, grant this to the player.
            //int coins = PlayerPrefs.GetInt("Coins", 0);
            //PlayerPrefs.SetInt("Coins",  coins + 800);
            //ScoreManager.instance.IAPAddDiamondCoinBalance(9000);
            //UIController.instance.coinAnimationDiamond.AddCoins(GameManager.instance.pack4Diamond.transform.position, 5);
            //GameManager.Instance.starsCollected = PlayerPrefs.GetInt("Coins", 0);
            //PlayerPrefs.SetInt("numberOfDeaths", 4);
            GameManager.instance.currencyManager.AddCurrency(9000);
        }
        // Or ... an unknown product has been purchased by this user. Fill in additional products here....
        else
        {
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
        }
        if (String.Equals(args.purchasedProduct.definition.id, noAds, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // TODO: The subscription item has been successfully purchased, grant this to the player.
            //int coins = PlayerPrefs.GetInt("Coins", 0);
            //PlayerPrefs.SetInt("Coins",  coins + 800);
            //CurrencyManager.instance.AddCoinBalance(800);
            //GameManager.Instance.starsCollected = PlayerPrefs.GetInt("Coins", 0);
            //PlayerPrefs.SetInt("numberOfDeaths", 4);
            PlayerPrefs.SetInt("SHOW_ADS", 1);
            noAdsButton.gameObject.SetActive(false);
            //noAdsPanel.gameObject.SetActive(false);
        }
        // Or ... an unknown product has been purchased by this user. Fill in additional products here....
        else
        {
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
        }
        if (String.Equals(args.purchasedProduct.definition.id, purchaseAllMouseDragons, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // TODO: The subscription item has been successfully purchased, grant this to the player.
            for (int i = 11; i <= 20; i++)
            {
                PlayerPrefs.SetInt("PlayerNumber" + i, i);
            }
            //for (int i = 15; i <= 30; i++)
            //{
            //    PlayerPrefs.SetInt("PlayerNumberAnimals" + i, i);
            //}
            //PlayerPrefs.SetInt("PurchaseAllMouseDragons", 1);
            //purchaseAllMouseDragonsGo.gameObject.SetActive(false);
            //Shop.instance.PurchaseAllPack();
            //CheckingPurchases();
        }if (String.Equals(args.purchasedProduct.definition.id, purchaseAll, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // TODO: The subscription item has been successfully purchased, grant this to the player.
            for (int i = 1; i <= 30; i++)
            {
                PlayerPrefs.SetInt("PlayerNumber" + i, i);
            }
            //PlayerPrefs.SetInt("PurchaseAllMouseDragons", 1);
            //PlayerPrefs.SetInt("PurchaseAllTigerDragons", 1);
            //PlayerPrefs.SetInt("PurchaseAllSeaDragons", 1);
            //Shop.instance.PurchaseAllPack();
            //CheckingPurchases();
        }
        if (String.Equals(args.purchasedProduct.definition.id, purchaseAllTigerDragons, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // TODO: The subscription item has been successfully purchased, grant this to the player.
            for (int i = 1; i <= 10; i++)
            {
                PlayerPrefs.SetInt("PlayerNumber" + i, i);
            }
            //for (int i = 15; i <= 30; i++)
            //{
            //    PlayerPrefs.SetInt("PlayerNumberAnimals" + i, i);
            //}
            //PlayerPrefs.SetInt("PurchaseAllTigerDragons", 1);
            //purchaseAllTigerDragonsGo.gameObject.SetActive(false);
            //Shop.instance.PurchaseAllPack();
            //CheckingPurchases();
        }
        if (String.Equals(args.purchasedProduct.definition.id, purchaseAllSeaDragons, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            // TODO: The subscription item has been successfully purchased, grant this to the player.
            for (int i = 20; i <= 30; i++)
            {
                PlayerPrefs.SetInt("PlayerNumber" + i, i);
            }
            //for (int i = 15; i <= 30; i++)
            //{
            //    PlayerPrefs.SetInt("PlayerNumberAnimals" + i, i);
            //}
            //PlayerPrefs.SetInt("PurchaseAllSeaDragons", 1);
            //purchaseAllSeaDragonsGo.gameObject.SetActive(false);
            //Shop.instance.PurchaseAllPack();
            //CheckingPurchases();
        }
        // Or ... an unknown product has been purchased by this user. Fill in additional products here....
        else
        {
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
        }
        // Return a flag indicating whether this product has completely been received, or if the application needs 
        // to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
        // saving purchased products to the cloud, and when that save is delayed. 
        return PurchaseProcessingResult.Complete;
    }


    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        // A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
        // this reason with the user to guide their troubleshooting actions.
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }
    public void CheckingPurchases()
    {
        if (PlayerPrefs.GetInt("PurchaseAllMouseDragons") == 1)
        {
            purchaseAllMouseDragonsGo.gameObject.SetActive(false);
        }
        if (PlayerPrefs.GetInt("PurchaseAllTigerDragons") == 1)
        {
            purchaseAllTigerDragonsGo.gameObject.SetActive(false);
        }
        if (PlayerPrefs.GetInt("PurchaseAllSeaDragons") == 1)
        {
            purchaseAllSeaDragonsGo.gameObject.SetActive(false);
        }
        if (PlayerPrefs.GetInt("PurchaseAllMouseDragons") == 1 && PlayerPrefs.GetInt("PurchaseAllTigerDragons") == 1 && PlayerPrefs.GetInt("PurchaseAllSeaDragons") == 1)
        {
            unlockPurchaseAllButtonMainMenu.SetActive(false);
            unlockPurchaseAllButtonShop.SetActive(false);
            purchasePanel.SetActive(false);
        }
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        throw new NotImplementedException();
    }
}

