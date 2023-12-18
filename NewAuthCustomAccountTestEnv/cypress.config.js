const { defineConfig } = require("cypress");

module.exports = defineConfig({
    e2e: {
        "baseUrl": "https://localhost:7254/",
        "defaultCommandTimeout": 5000,
        "video": false,
        "env": {
            "staging": true
        },

        setupNodeEvents(on, config) {
            // implement node event listeners here
        },
        "experimentalRunAllSpecs": true,
        "experimentalOriginDependencies": false,
    },
    "experimentalCspAllowList": true,
    "experimentalFetchPolyfill": true,
    "experimentalInteractiveRunEvents": true,
    "experimentalMemoryManagement": true,
    "experimentalModifyObstructiveThirdPartyCode": true,

    "experimentalSourceRewriting": true,
    "experimentalStudio": true,
    "experimentalWebKitSupport": true,
});