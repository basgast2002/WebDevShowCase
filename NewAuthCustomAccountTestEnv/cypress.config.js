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
            config.experimentalStudio = true;
        },
    },
});