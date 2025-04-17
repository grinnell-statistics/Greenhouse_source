var codapPlugin= {
    Init: function()
    {
        Farmtistics.codapPhone.call({
            "action": "update",
            "resource": "interactiveFrame",
            "values":
            {
                "name": "Farmer",
                "version": "1.0",
                "preventBringToFront": false,
                "preventDataContextReorg": false,
                "cannotClose": true,
                "dimensions":
                {
                    "width": 1000,
                    "height": 580
                }
            }
        }, function(){console.log("Initializing game")});
    },

    CreateDataContext: function()
    {
        Farmtistics.codapPhone.call({
            "action": "create",
            "resource": "dataContext",
            "values": {
                "name": "SessionData",
                "title": "Session Data",
                "collections": [
                {
                    "name": "Games",
                    "attrs": [
                        { "name": "gameNum", "type": "numeric", "precision": 0 },
                        { "name": "season", "type": "numeric", "precision": 0 }
                    ]
                },
                {
                    "name": "Farmer",
                    "parent": "Games",
                    "labels": {
                        "singleCase": "tower",
                        "pluralCase": "towers"
                    },
                    "attrs": [
                        { "name": "playerID", "type": "categorical"},
                        { "name": "groupID", "type": "categorical"},
                        { "name": "gold", "type": "numeric", "precision": 0 },
                        { "name": "rain", "type": "numeric", "precision": 0 },
                        { "name": "water", "type": "numeric", "precision": 0 },
                        { "name": "nitrate", "type": "numeric", "precision": 0 },
                        { "name": "fertilizer", "type": "numeric", "precision": 0 },
                        { "name": "croptype", "type": "categorical" }
                    ]
                }]
            }
        }, function(){console.log("Creating session data")});
    },

    SendGameData: function(gameNum, level)
    {
        Farmtistics.codapPhone.call({
            "action": "create",
            "resource": "dataContext[SessionData].collection[Games].case",
            "values":[
                {
                    "values": {
                        "gameNum": gameNum,
                        "level": level
                    }
                }
            ]
        }, function(result){
            if(result !== undefined)
            {
                openGameCase = result.values[0].id;
                console.log(result.success);
                console.log("I have caseID" + result.values[0].id);
            }
        });
    },

    SendLevelData: function(playerID, groupID, gold, rain, water, nitrate, fertilizer, croptype )
    {
        DefendersGame.codapPhone.call({
            "action": "create",
            "resource": "dataContext[SessionData].collection[Farmer].case",
            "values":[
                {
                    "parent": openGameCase,
                    "values": {
                        "playerID": Pointer_stringify(playerID),
                        "groupID": Pointer_stringify(groupID),
                        "gold": gold,
                        "rain": rain,
                        "water": water,
                        "nitrate": nitrate,
                        "fertilizer": fertilizer,
                        "croptype": Pointer_stringify(croptype)
                    }
                }
            ]
        }, function(){console.log("CODAP Data Sent")});
    },

    CODAPSendDataAll: function()
    {
        Farmtistics.codapPhone.call({
            "action": "create",
            "resource": "dataContextFromURL",
            "values": {
                "name": "All Data",
                "URL": "https://stat2games.sites.grinnell.edu/data/farmer/getdata.php"
            }
        }, function(){console.log("Send data")});
    },

    CODAPSendDataPlayer: function(playerID)
    {
        var str = "https://stat2games.sites.grinnell.edu/data/farmer/getdata.php?player=" + Pointer_stringify(playerID);
        console.log(playerID);
        Farmtistics.codapPhone.call({
            "action": "create",
            "resource": "dataContextFromURL",
            "values": {
                "name": "Player Data",
                "URL": str
            }
        }, function(){console.log("Send player data")});
    },

    CODAPSendDataGroup: function(groupID)
    {
        var str = "https://stat2games.sites.grinnell.edu/data/farmer/getdata.php?group=" + Pointer_stringify(groupID);
        console.log(groupID);
        Farmtistics.codapPhone.call({
            "action": "create",
            "resource": "dataContextFromURL",
            "values": {
                "name": "Group Data",
                "URL": str
            }
        }, function(){console.log("Send group data")});
    }
};

mergeInto(LibraryManager.library, CODAPManagerPlugin);
