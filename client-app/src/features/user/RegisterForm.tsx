import React, { useContext } from "react";
import { Form as FinalForm, Field } from "react-final-form";
import { Form, Button, Header } from "semantic-ui-react";
import TextInput from "../../app/common/form/TextInput";
import { RootStoreContext } from "../../app/stores/rootStore";
import { IUserFormValues } from "../../app/models/user";
import { FORM_ERROR } from "final-form";
import { combineValidators, isRequired } from "revalidate";
import ErrorMessage from "../../app/common/form/ErrorMessage";

const validate = combineValidators({
  email: isRequired("email"),
  displayName: isRequired("display name"),
  username: isRequired("username"),
  password: isRequired("password")
});

const RegisterForm = () => {
  const rootStore = useContext(RootStoreContext);
  const { register } = rootStore.userStore;

  return (
    <FinalForm
      onSubmit={(values: IUserFormValues) =>
        register(values).catch(error => ({
          [FORM_ERROR]: error
        }))
      }
      validate={validate}
      render={({
        handleSubmit,
        submitting,
        submitError,
        invalid,
        pristine,
        dirtySinceLastSubmit
      }) => (
        <Form onSubmit={handleSubmit} error>
          <Header as='h2' content='Sign up to Reactivities' color='teal' textAlign='center' />
          <Field name='username' component={TextInput} placehodler='Username' />
          <Field name='displayName' component={TextInput} placehodler='Display Name' />
          <Field name='email' component={TextInput} placehodler='Email' />
          <Field name='password' component={TextInput} placehodler='password' type='password' />
          {submitError && !dirtySinceLastSubmit && <ErrorMessage error={submitError} />}
          <Button
            disabled={(invalid && !dirtySinceLastSubmit) || pristine}
            loading={submitting}
            color='teal'
            content='Register'
            fluid
          />
          {/* <pre>{JSON.stringify(form.getState(), null, 2)}</pre> */}
        </Form>
      )}
    />
  );
};

export default RegisterForm;
