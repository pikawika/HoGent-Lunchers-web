var base_url = "https://localhost:5001/";

describe('lunchers gebruiker login test',function(){
    it('Check detail pagina als niet aangemeld', function(){
        cy.visit(base_url)
        cy.contains('Meer info').click()
        cy.url().should('include','/login')
    })

    it('login pagina', function(){
        cy.visit(base_url+'login') 
        cy.get('#username').type('lennert')
        cy.get('#password').type('Wachtwoord123')
        cy.get('button').contains('Login').click()
    })

    it('check of ingelogged is', function(){
        cy.contains("Welkom lennert")
    })

    it('check of detail pagina nu beschikbaar is', function(){
       
        cy.contains('Meer info').click()
        cy.url().should('include','/details')
    })

    it('check of geen toegang tot handelaar pagina', function(){
        cy.contains('Welkom lennert').click()
        cy.url().should('eq', base_url)
    })

    it('check logout', function(){
        cy.contains("Logout").click()
        cy.contains("Login")
    })
})

describe('lunchers handelaar login test',function(){

    it('login pagina', function(){
        cy.visit(base_url+'login') 
        cy.get('#username').type('qarfa')
        cy.get('#password').type('Wachtwoord123')
        cy.get('button').contains('Login').click()
    })

    it('check of ingelogged is', function(){
        cy.contains("Welkom qarfa")
    })

    it('check of toegang tot handelaar pagina', function(){
        cy.contains('Welkom qarfa').click()
        cy.url().should('include', "merchant/lunch")
    })

    it('check logout', function(){
        cy.contains("Logout").click()
        cy.contains("Login")
    })
})

