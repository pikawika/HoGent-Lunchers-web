const base_url = "https://localhost:5001/";
const username = "testgebruiker"
const password = "Wachtwoord123"
describe('lunchers toevoegen', function () {


    it('voeg lunch toe', function () {

        cy.visit(base_url + 'login')
        cy.get('#username').type(username)
        cy.get('#password').type(password)
        cy.get('button').contains('Login').click()

        cy.contains("Voeg een lunch toe").click()
        cy.get('#name').type('lunchtest')
        cy.get('#price').type('10')
        cy.get('#description').type('Test beschrijving')

        cy.get("input[type=file]").then(subject => {
            return cy.fixture("lunch.jpg", 'base64')
                .then(Cypress.Blob.base64StringToBlob)
                .then(blob => {
                    const el = subject[0]
                    const nameSegments = "lunch.jpg".split('/')
                    const name = nameSegments[nameSegments.length - 1]
                    const testFile = new File([blob], name, {type:"image/jpeg"})
                    const dataTransfer = new DataTransfer()
                    dataTransfer.items.add(testFile)
                    el.files = dataTransfer.files
                    return subject
                })
        })

        cy.contains("Voeg lunch toe").click()
        cy.contains("lunchtest")
    })

    it('edit lunch', function () {

        cy.visit(base_url + 'login')
        cy.get('#username').type(username)
        cy.get('#password').type(password)
        cy.get('button').contains('Login').click()

        cy.get('#name').type("lunchtest1")
        cy.contains('Pas lunch aan').click()
        cy.contains("lunchtest1")

    })

    it('verwijder lunch', function () {

        cy.visit(base_url + 'login')
        cy.get('#username').type(username)
        cy.get('#password').type(password)
        cy.get('button').contains('Login').click()

        cy.get('.removeLunchButton').click()

        cy.contains("lunchtest")
    })

})

