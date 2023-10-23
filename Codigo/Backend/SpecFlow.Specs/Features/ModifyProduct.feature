Feature: Modify Product Information
    As a pharmacy employee
    I want to be able to modify the information of existing products in the system
    So I can keep the catalog updated and improve the quality of the information available for customers.

Scenario: Successfully Modify an Existing Product's Details
    Given I am a pharmacy employee
    And I choose an existing product to modify
    And I enter the new valid code
    And I enter the new valid name
    And I enter the new valid description
    And I enter the new valid price
    When I request the product modification
    Then the system validates the new information
    And updates the database with the new product details
    And it displays a confirmation message indicating that the modification has been successfully carried out

Scenario: Attempt to Modify a Non-existent Product
    Given I am a pharmacy employee
    And I choose a product to modify that does not exist in the database
    And I enter the new valid code
    And I enter the new valid name
    And I enter the new valid description
    And I enter the new valid price
    When I choose the option to modify a product
    Then the system informs that the product does not exist

Scenario Outline: Modifying product details with specific data
    Given I am a pharmacy employee
    And I choose an existing product to modify
    And I enter the new code <code>
    And I enter the new name <name>
    And I enter the new description <description>
    And I enter the new valid price
    When I choose the option to modify a product
	Then the system shows the error message <errorMessage>

 Examples: 
    | code          | name                                     | description                                                                 | errorMessage                                               |
    | '44444'       | 'Valid Name'                             | 'Valid Description'                                                         | 'The new product code already exists in that pharmacy.'    |
    | '1234A'       | 'Valid Name'                             | 'Valid Description'                                                         | 'The product code is invalid.'                             |
    | '12222'       | 'Name that exceeds 30 charsssssssssss'   | 'Valid Description'                                                         | 'The product name is too long.'                            |
    | '12222'       | 'Valid Name'                             | 'Description that exceeds 70 charssssssssssssssssssssssssssssssssssssssss'  | 'The product description is too long.'                     |
