Feature: CreateProduct

  As a pharmacy employee
  I want to be able to add new products to the system
  So that customers can explore and purchase a wider variety of available products

  Scenario: Successful Product Creation
    Given I am an authorized pharmacy employee
    And I enter a valid code for a new product
    And I enter a valid name
    And I enter a valid description
    And I enter a valid price
    When I request its addition
    Then the system validates that all data is present and correct
    And the system inserts the new product into the product database
    And the system displays a confirmation message indicating the product was successfully added to the catalog

  Scenario: Duplicate Code
    Given I am an authorized pharmacy employee
    And the system already has a product with the same code I enter
    And I enter a valid name
    And I enter a valid description
    And I enter a valid price
    When I attempt to add the product
    Then the system shows an error message indicating the product code already exists and must be unique

  Scenario Outline: Invalid or Missing Product Data
    Given I am an authorized pharmacy employee
    And I enter the code <code> for the product
    And I enter the name <name> for the product
    And I enter the description <description> for the product
    And I enter the price <price> for the product
    When I attempt to add the product
    Then the system shows an error message indicating <errorMessage>

  Examples:
    | code      | name                                    | description                                                                      | price        | errorMessage                                    |
    | ''        | 'Valid Name'                            | 'Valid Description'                                                              | '10.5'       | 'Mandatory information is missing'              |
    | '1234A'   | 'Valid Name'                            | 'Valid Description'                                                              | '10.5'       | 'The product code is invalid'                   |
    | '12345'   | 'Name that exceeds 30 charsssssssssss'  | 'Valid Description'                                                              | '10.5'       | 'The product name is too long'                  |
    | '12345'   | 'Valid Name'                            | 'Description that exceeds 70 charssssssssssssssssssssssssssssssssssssssss'       | '10.5'       | 'The product description is too long'           |
    | '12345'   | 'Valid Name'                            | 'Valid Description'                                                              | 'Invalid'    | 'The price must be a valid decimal value'       |
