﻿namespace ConfluenceWebApiUnitTest;

public abstract class ConfluenceBaseUnitTest
{
    protected static readonly CultureInfo culture = new CultureInfo("en-US");

    protected const string storeKey = "confluence";
    //protected const string testRepoKey = "local-generic-test";
    //protected const PackageType testRepoType = PackageType.Generic;
    protected const string appName = "UnitTest";


    //protected static readonly string testHost = KeyStore.Key(storeKey)!.Host!;
    protected static readonly string testLogin = KeyStore.Key(storeKey)!.Login!;
    protected static readonly string testUser = KeyStore.Key(storeKey)!.User!;
    //protected static readonly string testUserEmail = KeyStore.Key(storeKey)!.Email!;

    //protected static readonly Uri baseUri = new(testHost);
    //protected static readonly Uri apiUri = new(baseUri, "rest/api/2/");

    //protected static readonly string repoKeyDynamic = "local-generic-test-dynamic";
    //protected static readonly string repoKeyFix = "local-generic-test-fix";


    protected static readonly string testSpaceKey = "~" + KeyStore.Key(storeKey)!.Login!;
    protected static readonly string testSpaceTitle = KeyStore.Key(storeKey)!.User + "s Startseite";

    protected const string testPageBaseline = "478111079";

    protected const string testPageFix = "478097426";
    protected const string testPageFixAttachment = "478114423";
    protected const string testPageFixChildren = "478101610";
    protected const string testPageFixEmoji = "478114171";
    protected const string testPageFixLabel = "478097445";

    protected const string testPageDyn = "478097428";
    protected const string testPageDynAttachment = "478115004";
}