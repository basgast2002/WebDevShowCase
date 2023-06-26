describe('register -> log in -> delete account', () => {
    it('should make a new account', () => {
        //index -> Register -> add data -> confirm data -> confrim account creation
        cy.visit('https://localhost:7254/');
        cy.get('[data-cy="RegisterButton"]').click();
        cy.get('#Input_UserName').type('testuser');
        cy.get('#Input_Name').type('testuser');
        cy.get('#Input_Email').type('testuser@testuser.cy');
        cy.get('#Input_Password').type('testuser');
        cy.get('#Input_ConfirmPassword').type('testuser');
        cy.get('[data-cy="RegisterSubmitButton"]').click();
        cy.get('#confirm-link').click();
    });
    it('should login', () => {
        //index -> login -> add data -> confirm input
        cy.visit('https://localhost:7254/');
        cy.get('[data-cy="LoginButton"]').click();
        cy.get('#Input_Username').type('testuser');
        cy.get('#Input_Password').type('testuser');
        cy.get('[data-cy="LoginSubmitButton"]').click();
    });
    it('should log in and delete account', () => {
        //login procedure
        cy.visit('https://localhost:7254/');
        cy.get('[data-cy="LoginButton"]').click();
        cy.get('#Input_Username').type('testuser');
        cy.get('#Input_Password').type('testuser');
        cy.get('[data-cy="LoginSubmitButton"]').click();
        //index -> manage -> Personal data -> delete -> confirm delete

        cy.get('[data-cy="ManageAccountButton"]').click();

        cy.get('[data-cy="DeleteAccountMenuItemButton"]').click();
        cy.get('[data-cy="DeleteAccountButton"]').click();

        cy.get('#Input_Password').type('testuser');

        cy.get('[data-cy="DeleteButton"]').click();
    });
})