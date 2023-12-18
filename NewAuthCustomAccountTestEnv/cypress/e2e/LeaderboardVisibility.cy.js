describe('Privacy Settings test', () => {
    it('should not display', () => {
        cy.visit('https://localhost:7254')

        cy.get('[data-cy="RegisterButton"]').click();
        cy.get('#Input_UserName').clear('t');
        cy.get('#Input_UserName').type('testuser');
        cy.get('#Input_Name').clear();
        cy.get('#Input_Name').type('testuser');
        cy.get('#Input_Email').clear();
        cy.get('#Input_Email').type('testuser@testuser.cy');
        cy.get('#Input_Password').clear();
        cy.get('#Input_Password').type('testuser');
        cy.get('#Input_ConfirmPassword').clear();
        cy.get('#Input_ConfirmPassword').type('testuser');
        cy.get('[data-cy="RegisterSubmitButton"]').click();
        cy.get('#confirm-link').click();
        cy.get('[data-cy="LoginButton"]').click();
        cy.get('#Input_Username').clear('t');
        cy.get('#Input_Username').type('testuser');
        cy.get('#Input_Password').clear();
        cy.get('#Input_Password').type('testuser');
        cy.get('[data-cy="LoginSubmitButton"]').click();

        cy.get('.flex-grow-1 > :nth-child(3) > .nav-link').click();
        cy.get('.pb-3').contains("testuser").should('not.exist')
    });
    it('should display', () => {
        cy.visit('https://localhost:7254')

        cy.get('[data-cy="LoginButton"]').click();
        cy.get('#Input_Username').clear('t');
        cy.get('#Input_Username').type('testuser');
        cy.get('#Input_Password').clear();
        cy.get('#Input_Password').type('testuser');
        cy.get('[data-cy="LoginSubmitButton"]').click();

        cy.get('[data-cy="ManageAccountButton"]').click();
        cy.get('#Privacy').click();
        cy.get('div > form > .btn').click();

        cy.get('.flex-grow-1 > :nth-child(3) > .nav-link').click();
        cy.get('.pb-3').contains("testuser");
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