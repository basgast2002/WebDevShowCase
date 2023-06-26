describe('Admin can see admin button', () => {
    it('should login', () => {
        //index -> login -> add data -> confirm input
        cy.visit('https://localhost:7254/');
        cy.get('[data-cy="LoginButton"]').click();
        cy.get('#Input_Username').type('testuserAdmin');
        cy.get('#Input_Password').type('testuserAdmin');
        cy.get('[data-cy="LoginSubmitButton"]').click();
        cy.get('[data-cy="AdminPanelButton"]').click()
    });
})