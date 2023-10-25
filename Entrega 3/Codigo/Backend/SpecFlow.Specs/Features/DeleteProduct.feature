Feature: DeleteProduct

As a pharmacy employee
I want to be able to delete products from the system
To manage the catalogue and keep the information up to date


  Scenario: Delete an Existing Product

    Given that I am an authorized pharmacy employee
    And I select the option to delete a product
    And the system displays a list of existing products
    When I delete the product
    Then the system marks the product as "unavailable" in the database
    And displays a confirmation message indicating that the product has been successfully deleted

  Scenario: Attempt to delete a Nonexistent Product

    Given that I am an authorized pharmacy employee
    And I select the option to delete a product
    When I attempt to delete a product that does not exist in the system
    Then the system informs me that the product does not exist
    And takes no action

#Maybe delete this scenario? Its front end exclusive and all steps are empty
  Scenario: Cancel Product Deactivation

    Given that I am an authorized pharmacy employee
    And I am in the process of deactivating a product
    When I cancel the operation
    Then the system makes no modifications to the database

